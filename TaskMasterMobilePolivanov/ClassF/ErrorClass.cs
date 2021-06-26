using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMasterMobilePolivanov.ClassF
{
    class ErrorClass
    {
        public static void MessageForUser(string mess)
        {
            PageF.ErrorWindow errorWindow = new PageF.ErrorWindow(mess);
            errorWindow.Show();
        }
    }
}
