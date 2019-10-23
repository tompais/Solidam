using Enums;
using Exceptions;
using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Helpers;
using Utils;

namespace Services
{
    public class TestService : BaseService<TestService>
    {
        public TestService()
        {
        }


        public static List<Usuarios> Get()
        {
            return Db.Usuarios.ToList();
        }
    }
}