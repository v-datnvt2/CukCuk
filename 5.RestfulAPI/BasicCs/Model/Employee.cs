using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCs.Model
{
    class Employee:Person
    {

        #region field
        private string _fullName;

        public Employee(string fullName, string identityNumber, int age) : base(identityNumber, age)
        {
            this.FullName = fullName;
            this.IdentityNumber = identityNumber;
            this.Age = age;
        }

        #endregion

        #region props


        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public Gender? Gender { get; set; }


        #endregion
    }
}
