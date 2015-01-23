using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace WPFLogin.Helpers
{
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty InputPassword =
            DependencyProperty.RegisterAttached("InputPassword", typeof(SecureString), typeof(PasswordBoxHelper),
                new PropertyMetadata(null, OnInputPasswordChanged));

        public static readonly DependencyProperty AttachPassword = DependencyProperty.RegisterAttached(
            "AttachPassword", typeof(bool), typeof(PasswordBoxHelper),
            new PropertyMetadata(false, OnAttachPasswordChanged));

        private static readonly DependencyProperty UpdatingPassword =
            DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool), typeof(PasswordBoxHelper),
                new PropertyMetadata(false));

        private static void OnInputPasswordChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs args)
        {
            var box = dependencyObject as PasswordBox;

            if (dependencyObject == null || !GetAttachPassword(dependencyObject))
            {
                return;
            }

            // avoid recursive updating by ignoring the control changed event
            box.PasswordChanged -= HandlePasswordChanged;

            var newPassword = (SecureString)args.NewValue;

            if (!GetUpdatingPassword(box))
            {
                box.Password = (newPassword == null) ? string.Empty : newPassword.ToString();
            }

            box.PasswordChanged += HandlePasswordChanged;
        }

        private static void OnAttachPasswordChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs args)
        {
            var passwordBox = dependencyObject as PasswordBox;

            if (passwordBox == null)
                return;

            var wasBound = (bool)(args.OldValue);
            var needToBind = (bool)(args.NewValue);

            if (wasBound)
                passwordBox.PasswordChanged -= HandlePasswordChanged;

            if (needToBind)
                passwordBox.PasswordChanged += HandlePasswordChanged;
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs args)
        {
            var box = sender as PasswordBox;
            SetUpdatingPassword(box, true);
            SetInputPassword(box, box.SecurePassword);
            SetUpdatingPassword(box, false);
        }

        public static void SetAttachPassword(DependencyObject dependancyProperty, bool value)
        {
            dependancyProperty.SetValue(AttachPassword, value);
        }

        public static bool GetAttachPassword(DependencyObject dependancyProperty)
        {
            return (bool)dependancyProperty.GetValue(AttachPassword);
        }

        public static string GetInputPassword(DependencyObject dependancyProperty)
        {
            return (string)dependancyProperty.GetValue(InputPassword);
        }

        public static void SetInputPassword(DependencyObject dependancyProperty, SecureString value)
        {
            dependancyProperty.SetValue(InputPassword, value);
        }

        private static bool GetUpdatingPassword(DependencyObject dependancyProperty)
        {
            return (bool)dependancyProperty.GetValue(UpdatingPassword);
        }

        private static void SetUpdatingPassword(DependencyObject dependancyProperty, bool value)
        {
            dependancyProperty.SetValue(UpdatingPassword, value);
        }
    }
}
