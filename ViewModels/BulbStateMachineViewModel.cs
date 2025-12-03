using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;
using Stateless;
using Stateless.Graph;

namespace WpfStateMachineDemo;

// 定义状态和触发器
public enum BulbState
{
    Off,
    On,
    Dimmed,
    Blinking
}

public enum BulbTrigger
{
    TurnOn,
    TurnOff,
    Toggle,
    SwitchMode,
    Reset
}

public class BulbStateMachineViewModel : INotifyPropertyChanged
{
    private DispatcherTimer _blinkingTimer;
    private Brush _bulbColor;
    private bool _isBlinkingOn;
    private BitmapImage _stateDiagramImage;
    private StateMachine<BulbState, BulbTrigger> _stateMachine;
    private string _statusMessage;

    public BulbStateMachineViewModel()
    {
        InitializeStateMachine();
        InitializeCommands();
        InitializeBlinkingTimer();

        // 初始状态
        CurrentState = BulbState.Off;
        BulbColor = Brushes.Gray;
        StatusMessage = "灯泡已关闭";

        // 记录初始状态
        StateHistory.Add($"{DateTime.Now:HH:mm:ss} - 初始状态: {CurrentState}");
    }

    // 属性
    public BulbState CurrentState
    {
        get => _stateMachine.State;
        private set
        {
            OnPropertyChanged(nameof(CurrentState));
            OnPropertyChanged(nameof(CanTurnOn));
            OnPropertyChanged(nameof(CanTurnOff));
            OnPropertyChanged(nameof(CanToggle));
            OnPropertyChanged(nameof(CanSwitchMode));
            OnPropertyChanged(nameof(AvailableTriggers));
        }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            _statusMessage = value;
            OnPropertyChanged(nameof(StatusMessage));
        }
    }

    public Brush BulbColor
    {
        get => _bulbColor;
        set
        {
            _bulbColor = value;
            OnPropertyChanged(nameof(BulbColor));
        }
    }

    public BitmapImage StateDiagramImage
    {
        get => _stateDiagramImage;
        set
        {
            _stateDiagramImage = value;
            OnPropertyChanged(nameof(StateDiagramImage));
        }
    }

    public ObservableCollection<string> StateHistory { get; } = new();

    public string AvailableTriggers => string.Join(", ",
        _stateMachine.PermittedTriggers.Select(t => t.ToString()));

    // 命令属性
    public ICommand TurnOnCommand { get; private set; }
    public ICommand TurnOffCommand { get; private set; }
    public ICommand ToggleCommand { get; private set; }
    public ICommand SwitchModeCommand { get; private set; }
    public ICommand ResetCommand { get; private set; }
    public ICommand GenerateDiagramCommand { get; private set; }
    public ICommand ExportDiagramCommand { get; private set; }

    // 按钮可用性
    public bool CanTurnOn => _stateMachine.CanFire(BulbTrigger.TurnOn);
    public bool CanTurnOff => _stateMachine.CanFire(BulbTrigger.TurnOff);
    public bool CanToggle => _stateMachine.CanFire(BulbTrigger.Toggle);
    public bool CanSwitchMode => _stateMachine.CanFire(BulbTrigger.SwitchMode);

    public event PropertyChangedEventHandler PropertyChanged;

    private void InitializeStateMachine()
    {
        // 创建状态机实例
        _stateMachine = new StateMachine<BulbState, BulbTrigger>(BulbState.Off);

        // 配置状态转换
        _stateMachine.Configure(BulbState.Off)
            .Permit(BulbTrigger.TurnOn, BulbState.On)
            .Permit(BulbTrigger.Toggle, BulbState.On)
            .OnEntry(() => OnStateEntered(BulbState.Off))
            .OnExit(() => StatusMessage = "正在关闭灯泡...");

        _stateMachine.Configure(BulbState.On)
            .Permit(BulbTrigger.TurnOff, BulbState.Off)
            .Permit(BulbTrigger.Toggle, BulbState.Off)
            .Permit(BulbTrigger.SwitchMode, BulbState.Dimmed)
            .OnEntry(() => OnStateEntered(BulbState.On))
            .OnExit(() => StatusMessage = "正在打开灯泡...");

        _stateMachine.Configure(BulbState.Dimmed)
            .Permit(BulbTrigger.TurnOff, BulbState.Off)
            .Permit(BulbTrigger.SwitchMode, BulbState.Blinking)
            .OnEntry(() => OnStateEntered(BulbState.Dimmed))
            .OnExit(() => StatusMessage = "正在调暗灯泡...");

        _stateMachine.Configure(BulbState.Blinking)
            .Permit(BulbTrigger.TurnOff, BulbState.Off)
            .Permit(BulbTrigger.SwitchMode, BulbState.On)
            .OnEntry(() =>
            {
                OnStateEntered(BulbState.Blinking);
                StartBlinking();
            })
            .OnExit(() =>
            {
                StopBlinking();
                StatusMessage = "正在停止闪烁...";
            });

        // 所有状态都可以重置
        _stateMachine.Configure(BulbState.Off)
            .PermitReentry(BulbTrigger.Reset)
            .OnEntryFrom(BulbTrigger.Reset, () =>
            {
                StateHistory.Clear();
                StateHistory.Add($"{DateTime.Now:HH:mm:ss} - 状态已重置");
                StatusMessage = "状态机已重置";
            });
    }

    private void InitializeCommands()
    {
        TurnOnCommand = new RelayCommand(
            () => FireTrigger(BulbTrigger.TurnOn),
            () => _stateMachine.CanFire(BulbTrigger.TurnOn));

        TurnOffCommand = new RelayCommand(
            () => FireTrigger(BulbTrigger.TurnOff),
            () => _stateMachine.CanFire(BulbTrigger.TurnOff));

        ToggleCommand = new RelayCommand(
            () => FireTrigger(BulbTrigger.Toggle),
            () => _stateMachine.CanFire(BulbTrigger.Toggle));

        SwitchModeCommand = new RelayCommand(
            () => FireTrigger(BulbTrigger.SwitchMode),
            () => _stateMachine.CanFire(BulbTrigger.SwitchMode));

        ResetCommand = new RelayCommand(
            () => FireTrigger(BulbTrigger.Reset),
            () => true);

        GenerateDiagramCommand = new RelayCommand(
            GenerateStateDiagram,
            () => true);

        ExportDiagramCommand = new RelayCommand(
            ExportStateDiagram,
            () => true);
    }

    private void InitializeBlinkingTimer()
    {
        _blinkingTimer = new DispatcherTimer();
        _blinkingTimer.Interval = TimeSpan.FromMilliseconds(500);
        _blinkingTimer.Tick += (s, e) => ToggleBlinking();
    }

    // 私有方法
    private void FireTrigger(BulbTrigger trigger)
    {
        try
        {
            _stateMachine.Fire(trigger);
            CurrentState = _stateMachine.State;
        }
        catch (InvalidOperationException ex)
        {
            StatusMessage = $"无效操作: {ex.Message}";
        }
    }

    private void OnStateEntered(BulbState newState)
    {
        // 更新灯泡颜色
        switch (newState)
        {
            case BulbState.Off:
                BulbColor = Brushes.Gray;
                StatusMessage = "灯泡已关闭";
                break;
            case BulbState.On:
                BulbColor = Brushes.Yellow;
                StatusMessage = "灯泡已打开";
                break;
            case BulbState.Dimmed:
                BulbColor = Brushes.Orange;
                StatusMessage = "灯泡已调暗";
                break;
            case BulbState.Blinking:
                _isBlinkingOn = true;
                BulbColor = Brushes.Yellow;
                StatusMessage = "灯泡正在闪烁";
                break;
        }

        // 记录状态转换
        StateHistory.Add($"{DateTime.Now:HH:mm:ss} - 状态: {newState}");
    }

    private void StartBlinking()
    {
        _blinkingTimer.Start();
    }

    private void StopBlinking()
    {
        _blinkingTimer.Stop();
        _isBlinkingOn = false;
    }

    private void ToggleBlinking()
    {
        _isBlinkingOn = !_isBlinkingOn;
        BulbColor = _isBlinkingOn ? Brushes.Yellow : Brushes.Gray;
    }

    private void GenerateStateDiagram()
    {
        try
        {
            // 生成DOT格式的状态图
            var dotGraph = UmlDotGraph.Format(_stateMachine.GetInfo());

            // 在真实项目中，这里可以调用Graphviz将DOT转换为图片

            StatusMessage = "状态图已生成";
            StateHistory.Add($"{DateTime.Now:HH:mm:ss} - 生成了状态图");
        }
        catch (Exception ex)
        {
            StatusMessage = $"生成状态图失败: {ex.Message}";
        }
    }

    private void ExportStateDiagram()
    {
        var saveDialog = new SaveFileDialog
        {
            Filter = "DOT文件|*.dot|所有文件|*.*",
            FileName = "BulbStateMachine.dot"
        };

        if (saveDialog.ShowDialog() == true)
            try
            {
                var dotGraph = UmlDotGraph.Format(_stateMachine.GetInfo());
                File.WriteAllText(saveDialog.FileName, dotGraph);
                StatusMessage = $"状态图已导出到: {saveDialog.FileName}";
                StateHistory.Add($"{DateTime.Now:HH:mm:ss} - 导出了状态图");

                // 可选：自动打开文件
                Process.Start("notepad.exe", saveDialog.FileName);
            }
            catch (Exception ex)
            {
                StatusMessage = $"导出失败: {ex.Message}";
            }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

// 简单的RelayCommand实现
public class RelayCommand : ICommand
{
    private readonly Func<bool> _canExecute;
    private readonly Action _execute;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    public void Execute(object parameter)
    {
        _execute();
    }
}