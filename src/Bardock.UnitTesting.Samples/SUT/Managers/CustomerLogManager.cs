﻿using Bardock.UnitTesting.Samples.SUT.Entities;

namespace Bardock.UnitTesting.Samples.SUT.Managers
{
    public interface ICustomerLogManager
    {
        void LogCreate(Customer e);

        void LogUpdate(Customer e);
    }

    public class CustomerLogManager : BaseManager, ICustomerLogManager
    {
        public CustomerLogManager(string userName)
            : base(userName)
        {
        }

        public void LogCreate(Customer c)
        {
        }

        public void LogUpdate(Customer e)
        {
        }
    }
}