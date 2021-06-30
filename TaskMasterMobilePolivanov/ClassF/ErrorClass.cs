using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TaskMasterMobilePolivanov.ClassF
{
    class ErrorClass
    {
        public static void MessageForUser(string mess) 
        {
            PageF.ErrorWindow errorWindow = new PageF.ErrorWindow(mess);
            errorWindow.Show();
        } //МЕССЕДЖБОКС для ошибок

        public async static void ToolTipMessage(string toolMes)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.Content = "Не все поля заполнены!!!";
            toolTip.IsOpen = true;
            await Task.Delay(3500);
            toolTip.IsOpen = false;
        } //ТУЛТИП для сообщений пользователю
    }
}
