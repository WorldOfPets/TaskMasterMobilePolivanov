using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMasterMobilePolivanov.DataBaseF
{
    public partial class OrderComplate
    {
        public string pacientFullName{
            get {
                return $"{OrderInfo.Pacient.LastName} {OrderInfo.Pacient.Name} {OrderInfo.Pacient.MiddleName}";
            }
        } //ПОЛНОЕ ИМЯ ПАЦИЕНТА
        public string laborantFullName 
        {
            get
            {
                return $"{UserLab.LastName} {UserLab.Name} {UserLab.MiddleName}";
            }
        } //ПОЛНОЕ ИМЯ ЛАБОРАНТА
        public string FullPrice 
        {
            get
            {
                decimal labs = 0;
                List<PacientLabService> pacientLab = ClassF.databaseClass.DBCl.PacientLabService.Where(x => x.IdPacient == OrderInfo.IdPacient).ToList();
                foreach (PacientLabService labService in pacientLab)
                {
                    labs += Convert.ToDecimal( ClassF.databaseClass.DBCl.LabServices.FirstOrDefault(x => x.Code == labService.IdLabServices).Price);
                }
                return $"{labs}$";
            }
        } //ПОЛНАЯ ЦЕНА ЗА ВСЕ УСЛУГИ
    }
}
