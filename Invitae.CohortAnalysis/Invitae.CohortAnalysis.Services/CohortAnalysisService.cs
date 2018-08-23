using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invitae.CohortAnalysis.Domain.Models;
using Invitae.CohortAnalysis.Interfaces;

namespace Invitae.CohortAnalysis.Services
{
    public class CohortAnalysisService : ICohortAnalysisService
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;

        private List<Customer> _customerData;
        private List<Order> _orderData;

        public CohortAnalysisService(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
        }

        private void LoadData()
        {
            Task<List<Customer>> customerTask = Task.Factory.StartNew(() => _customerService.GetAllRecordsFromCsv());
            Task<List<Order>> orderTask = Task.Factory.StartNew(() => _orderService.GetAllRecordsFromCsv());

            Task.WaitAll(customerTask, orderTask);

            _orderData = orderTask.Result;
            _customerData = customerTask.Result; 
        }

        public void RunAnalysis()
        {
            this.LoadData();
        }
    }
}
