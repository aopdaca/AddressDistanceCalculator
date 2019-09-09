using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TestConsoleApp
{
    public class Address
    {
        public string _zip;
        public string _state;
        public string _city;
        public string _streetaddress;
        public int _addressId;


        public int AddressId
        {
            get => _addressId;
            set
            {
                if (value > 0)
                {
                    _addressId = value;
                }
                else
                {
                    throw new ArgumentException(String.Format("{0} is less than 0", value), "value");
                }
            }
        }

        public string StreetAddress
        {
            get => _streetaddress;
            set
            {
                if (value != null)
                {
                    _streetaddress = value;
                }
                else
                {
                    throw new ArgumentNullException(String.Format("{0} is Null", value), "value");
                }

            }
        }
        public string City
        {
            get => _city;
            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z]+$") && value != null)
                {
                    _city = value;
                }
                else if (value == null)
                {
                    throw new ArgumentNullException(String.Format("{0} is Null", value), "value");
                }
                else
                {
                    throw new ArgumentException(String.Format("{0} is not a string of only letters", value), "value");
                }

            }
        }
        public string State
        {
            get => _state;
            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z]+$") && value != null)
                {
                    _state = value;
                }
                else if (value == null)
                {
                    throw new ArgumentNullException(String.Format("{0} is Null", value), "value");
                }
                else
                {
                    throw new ArgumentException(String.Format("{0} is not a string of only letters", value), "value");
                }

            }
        }
        public string Zipcode
        {
            get => _zip;
            set
            {
                if (Regex.IsMatch(value, @"^[0-9]+$") && value.Length == 5)
                {
                    _zip = value;
                }
                else
                {
                    throw new ArgumentException(String.Format("{0} is not a string of 5 integers", value), "value");
                }

            }
        }
    }
}
