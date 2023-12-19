using Dbsys.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dbsys
{
    public enum ErrorCode
    {
        Error,
        Success
    }
    public class UserRepository
    {
        private employeemanageEntities db;

        public UserRepository()
        {
            db = new employeemanageEntities();
        }

        public ErrorCode FrmRegister(String FirstName, String LastName, String Email, String Address, String username, String password, int roles)
        {
            try
            {
                using (var db = new employeemanageEntities())
                {
                    var userInfo = new userInformation();
                    var userAcc = new userAccount();

                    userInfo.userInFname = FirstName;
                    userInfo.userInLname = LastName;
                    userInfo.userInEmail = Email;
                    userInfo.userInAddress = Address;
                    userAcc.username = username;
                    userAcc.password = password;
                    userAcc.roleId = roles;

                    db.userInformation.Add(userInfo);
                    db.userAccount.Add(userAcc);
                    db.SaveChanges();

                    return ErrorCode.Success;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ErrorCode.Error;
            }
        }//public ErrorCode

        public ErrorCode FrmCEO(String FirstName, String LastName, String Email, String Address, int newSalary)
        {
            try
            {
                using (var db = new employeemanageEntities())
                {
                    var userInfo = new userInformation();
                    var saLary = new salary();

                    userInfo.userInFname = FirstName;
                    userInfo.userInLname = LastName;
                    userInfo.userInEmail = Email;
                    userInfo.userInAddress = Address;
                    saLary.salary1 = newSalary;
                    db.SaveChanges();


                    db.userInformation.Add(userInfo);
                    _ = db.salary.Add(saLary).ToString();
                    db.SaveChanges();
                    return ErrorCode.Success;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ErrorCode.Error;
            }
        }//public ErrorCode

        public userAccount GetUserbyUsername(String username)
        {
            using (db = new employeemanageEntities())
            {
                return db.userAccount.Where(s => s.username == username).FirstOrDefault();
            }
        }//public userAccount

        public List<userAccount> userAccounts()
        {
            using (db = new employeemanageEntities())
            {
                return db.userAccount.ToList();
            }
        }

        internal ErrorCode FrmCEO(string text1, string text2, string text3, string text4, string v)
        {
            throw new NotImplementedException();
        }
    }
}
