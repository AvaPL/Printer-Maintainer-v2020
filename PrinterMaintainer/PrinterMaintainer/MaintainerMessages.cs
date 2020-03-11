using System.Windows.Forms;

namespace PrinterMaintainer
{
    public static class MaintainerMessages
    {
        private const string Caption = "Printer Maintainer";

        public static DialogResult ShowQuestion()
        {
            const string Message = "Print test page?";
            const MessageBoxButtons Buttons = MessageBoxButtons.YesNoCancel;
            const MessageBoxIcon Icon = MessageBoxIcon.Question;
            return MessageBox.Show(Message, Caption, Buttons, Icon);
        }

        public static void ShowError(string message)
        {
            const MessageBoxButtons Buttons = MessageBoxButtons.OK;
            const MessageBoxIcon Icon = MessageBoxIcon.Error;
            MessageBox.Show(message, Caption, Buttons, Icon);
        }
    }
}