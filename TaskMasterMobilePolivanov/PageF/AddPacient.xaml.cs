﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskMasterMobilePolivanov.PageF
{
    /// <summary>
    /// Логика взаимодействия для AddPacient.xaml
    /// </summary>
    public partial class AddPacient : Page
    {
        public AddPacient()
        {
            InitializeComponent();

            CmbPolice.SelectedValuePath = "Id";
            CmbPolice.DisplayMemberPath = "Name";
            CmbPolice.ItemsSource = ClassF.databaseClass.DBCl.InsurancePoliceType.ToList();
        }

        private void CmbPolice_DropDownClosed(object sender, EventArgs e)
        {
            TbCmb.Text = CmbPolice.Text;
            var bc = new BrushConverter();
            TbCmb.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
        }


        private void DpBirthdate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            TbData.Text = DpBirthdate.SelectedDate.Value.ToShortDateString();
            if (DpBirthdate.SelectedDate <= DateTime.Today)
            {
                var bc = new BrushConverter();
                TbData.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
            }
            else
            {
                TbData.BorderBrush = Brushes.Red;
            }
        }

        private void BtnAddPacietn_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            if (TbLastName.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51") &&
                TbName.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51") &&
                TbMiddleName.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51") &&
                TbData.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51") &&
                TbIEN.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51") &&
                TbPassportS.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51") &&
                TbPassportN.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51") &&
                TbCmb.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51") &&
                TbPoliceNumber.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51") &&
                TbLogin.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51") &&
                TbPassword.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51") &&
                TbEmail.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51") &&
                TbNumberPhone.BorderBrush == (Brush)bc.ConvertFrom("#FF498C51"))
            { 
            DataBaseF.InsurancePolicy policy = new DataBaseF.InsurancePolicy() { 
            Number = Convert.ToInt32(TbPoliceNumber.Text),
            IdType = (int)CmbPolice.SelectedValue
            };
            ClassF.databaseClass.DBCl.InsurancePolicy.Add(policy);

            DataBaseF.Pacient pacient = new DataBaseF.Pacient() { 
            LastName = TbLastName.Text,
            Name = TbName.Text,
            MiddleName = TbMiddleName.Text,
            Birthdate = DpBirthdate.SelectedDate,
            EIN = TbIEN.Text,
            PassportS = Convert.ToInt32(TbPassportS.Text),
            PassportN = Convert.ToInt32(TbPassportN.Text),
            Login = TbLogin.Text,
            Password = TbPassword.Text,
            Email = TbEmail.Text,
            PhoneNumber = TbNumberPhone.Text,
            IdInsurancePolicy = policy.Id
            };
            ClassF.databaseClass.DBCl.Pacient.Add(pacient);

            ClassF.databaseClass.DBCl.SaveChanges();
            }
        }
        private void TbLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tB = (TextBox)sender;
            if (tB.Name == TbEmail.Name)
            {
                if (tB.Text.Contains("@") && tB.Text.Length >= 4)
                {
                    var bc = new BrushConverter();
                    tB.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
                }
                else
                {
                    tB.BorderBrush = Brushes.Red;
                }
            }
            else if (tB.Name == TbPoliceNumber.Name || tB.Name == TbPassportN.Name || tB.Name == TbPassportS.Name)
            {
                tB.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
                if (tB.Text.Length == tB.MaxLength)
                {
                    var bc = new BrushConverter();
                    tB.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
                }
                else
                {
                    tB.BorderBrush = Brushes.Red;
                }
            }
            else if (tB.Name == TbNumberPhone.Name)
            {
                tB.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
                tB.BorderBrush = Brushes.Red;
                switch (tB.Text.Length)
                {
                    case 2:
                        tB.Text += "(";
                        tB.Focus();
                        tB.CaretIndex = tB.Text.Length;
                        break;
                    case 6:
                        tB.Text += ") ";
                        tB.Focus();
                        tB.CaretIndex = tB.Text.Length;
                        break;
                    case 11:
                        tB.Text += "-";
                        tB.Focus();
                        tB.CaretIndex = tB.Text.Length;
                        break;
                    case 14:
                        tB.Text += "-";
                        tB.Focus();
                        tB.CaretIndex = tB.Text.Length;
                        break;
                    case 17:
                        var bc = new BrushConverter();
                        tB.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
                        break;
                }
            }
            else if (tB.Name == TbIEN.Name)
            {
                tB.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput);
                tB.BorderBrush = Brushes.Red;
                switch(tB.Text.Length)
                {
                    case 2:
                        tB.Text += "-";
                        tB.Focus();
                        tB.CaretIndex = tB.Text.Length;
                        break;
                    case 10:
                        var bc = new BrushConverter();
                        tB.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
                        break;
                }
            }
            else if (tB.Text.Length >= 3)
            {
                var bc = new BrushConverter();
                tB.BorderBrush = (Brush)bc.ConvertFrom("#FF498C51");
            }
            else
            {
                tB.BorderBrush = Brushes.Red;
            }
        }

        public void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
