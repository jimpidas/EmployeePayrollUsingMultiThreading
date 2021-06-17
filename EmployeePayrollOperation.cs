using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeePayrollUsingThreads
{
    
    public class EmployeePayrollOperation
    {
        static PerformanceCounter myCounter;
        public List<EmployeeDetails> employeePayrollDetailsList = new List<EmployeeDetails>();
        public void addEmployeeToPayroll(List<EmployeeDetails> employeePayrollDetailsList)
        {
            employeePayrollDetailsList.ForEach(employeeData =>
            {
                Console.WriteLine("Employee being added: " + employeeData.EmployeeName);
                this.addEmployeeToPayroll(employeeData);
                Console.WriteLine("Employee added: " + employeeData.EmployeeName);
            });
            Console.WriteLine(this.employeePayrollDetailsList.ToString());
            
        }

        public void Display()
        {
            foreach(EmployeeDetails emp in this.employeePayrollDetailsList)
            {
                Console.WriteLine(emp.EmployeeID + "    " + emp.EmployeeName + "    " + emp.Address + " " + emp.BasicPay + "    " + emp.City + "    " + emp.Country + "     " + emp.Deduction + "   " + emp.Department+"\n");
            }
        }


        public void addEmployeeToPayroll(EmployeeDetails emp)
        {
            employeePayrollDetailsList.Add(emp);
        }

        public void addEmployeeToPayrollWithThread(List<EmployeeDetails> employeePayrollDetailsList)
        {
           
            if (!PerformanceCounterCategory.Exists("Processor"))
            {
                Console.WriteLine("Object Processor does not exist!");
                return;
            }
            if (!PerformanceCounterCategory.CounterExists(@"% Processor Time", "Thread"))
            {
                Console.WriteLine(@"Counter % Processor Time does not exist!");
                return;
            }
            myCounter = new PerformanceCounter("Processor", @"% Processor Time", @"_Total");
            Console.WriteLine(@"Before inserting, %Processor Time, _Total= " + myCounter.NextValue().ToString());
            employeePayrollDetailsList.ForEach(employeeData =>
            {
                //Console.WriteLine("abc");
                Task thread = new Task(() =>
                {
                    Console.WriteLine("Employee being added: " + employeeData.EmployeeName);
                    this.addEmployeeToPayroll(employeeData);
                    Console.WriteLine("Employee added: " + employeeData.EmployeeName);
                });
                thread.Start();
                Thread.Sleep(1000);
            });
            Console.WriteLine(this.employeePayrollDetailsList.Count);
            Console.WriteLine(@"Current value of Processor, %Processor Time, _Total= " + myCounter.NextValue().ToString());
           
            /*while (true)
            {
                Console.WriteLine("@");

                try
                {
                    Console.WriteLine(@"Current value of Processor, %Processor Time, _Total= " + myCounter.NextValue().ToString());
                }

                catch
                {
                    Console.WriteLine(@"_Total instance does not exist!");
                    return;
                }

                Thread.Sleep(1000);
                Console.WriteLine(@"Press 'CTRL+C' to quit...");
            }*/
        }

    }

}
