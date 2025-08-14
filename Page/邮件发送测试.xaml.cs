using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Net;
using System.Net.Mail;

namespace Test;

public partial class 邮件发送测试 : Window
{
    public 邮件发送测试()
    {
        InitializeComponent();
    }
    public void SendEmail(string fromEmail, string password, string toEmail, string subject, string body)
    {
        try
        {
            // 创建一个MailMessage对象
            MailMessage mail = new MailMessage();

            // 设置发件人地址
            mail.From = new MailAddress(fromEmail);

            // 设置收件人地址
            mail.To.Add(toEmail);

            // 设置邮件主题
            mail.Subject = subject;

            // 设置邮件正文
            mail.Body = body;

            // 创建一个SmtpClient对象，用于发送邮件
            SmtpClient smtpClient = new SmtpClient("smtp.qq.com", 587); // 替换为实际的SMTP服务器和端口

            // 设置SMTP客户端的认证信息
            smtpClient.Credentials = new NetworkCredential(fromEmail, password);

            // 启用SSL加密
            smtpClient.EnableSsl = true;

            // 发送邮件
            smtpClient.Send(mail);

            // 邮件发送成功
            MessageBox.Show("邮件发送成功！");
        }
        catch (Exception ex)
        {
            // 处理发送邮件时的异常
            MessageBox.Show("邮件发送失败：" + ex.Message);
        }
    }

    private void SendButton_Click(object sender, RoutedEventArgs e)
    {
        string fromEmail = FromEmailTextBox.Text;
        string password = PasswordTextBox.Password;
        string toEmail = ToEmailTextBox.Text;
        string subject = SubjectTextBox.Text;
        string body = BodyTextBox.Text;

        SendEmail(fromEmail, password, toEmail, subject, body);
    }
}