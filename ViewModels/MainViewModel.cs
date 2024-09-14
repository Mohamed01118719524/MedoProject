using Medo.Data;
using Medo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Medo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly DataContext _context;
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }

        public MainViewModel(DataContext context)
        {
            _context = context;
            LoadData();
        }

        private void LoadData()
        {
            Orders = new ObservableCollection<Order>(_context.Orders.Include(o => o.Employee).ToList());
            Employees = new ObservableCollection<Employee>(_context.Employees.ToList());
            OnPropertyChanged(nameof(Orders));
            OnPropertyChanged(nameof(Employees));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}
