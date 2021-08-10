using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCs.Model
{
    class Person
    {

        #region field 
        private string _identityNumber;

        private int _age;

        public Person(string identityNumber, int age)
        {
            IdentityNumber = identityNumber;
            Age = age;
        }
        #endregion


        #region props

        public string IdentityNumber
        {
            get { return _identityNumber; }
            set { _identityNumber = value; }
        }



        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }


        #endregion



        #region methods

        #endregion
    }
}
