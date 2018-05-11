﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BleedifyModels.ModelsEF;
using BleedifyPersonal.Views;
using BleedifyServices;
using DomainViewModels;
using DomainViewModels.Commands;

namespace BleedifyPersonal.ViewModels
{
    public class DonatieDetailViewModel : BaseViewModel
    {
       public  DonatieViewModel DonatieViewModel { get; set; }
       public DonatieMasterDetailView view { get; set; }
       public ICommand AddCommand { get; private set; }

       public DonatieDetailViewModel(DonatieViewModel donatieViewModel)
       {
        
            DonatieViewModel = donatieViewModel;
            AddCommand = new BasicCommand(Save);
       }

       public IList<InstitutieAsociata> Institutii
       {
            get
            {
                return AppService.Instance.InstitutieAsociataService.GetAll().ToList();
            }
       }

        private InstitutieAsociata _selectedInstitutie;
        public InstitutieAsociata SelectedInstitutie
        {
            get
            {
                return _selectedInstitutie;
            }
            set
            {
                SetValue(ref _selectedInstitutie, value);
            }
        }
        public string NumeDonator { get; set; }


        public void Save()
        {
            var test = DonatieViewModel.DataDonare;
            var test2 = DonatieViewModel.EtapaDonare;
            var test3 = SelectedInstitutie;
            if (String.IsNullOrEmpty(NumeDonator) ||
               DonatieViewModel.DataDonare == null ||
               String.IsNullOrEmpty(DonatieViewModel.EtapaDonare) ||
               SelectedInstitutie == null)
            {
                MessageBox.Show("Plase make sure you completed all the fields correctly..", "Error", MessageBoxButton.OK);
            }

            try
            {
                Donator Donator = AppService.Instance.DonatorService.getDonatorByName(NumeDonator);
                DonatieViewModel.DonatorId = Donator.Id;
                DonatieViewModel.GrupaDeSangeId = Donator.GrupaDeSange;
                DonatieViewModel.GrupaDeSange = Donator.GrupaDeSangeObj;
                DonatieViewModel.InstitutieAsociataId = SelectedInstitutie.Id;

                MessageBox.Show(DonatieViewModel.ToString());
            }
            catch (ServiceException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
            }
        }
    }
}