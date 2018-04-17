using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Edge_FtpMgr
{
    public class Startup
    {
        /// <summary>
        /// Asynchrously create local account
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<object> CreateLocalAccountAsync(dynamic input)
        {
            string userName = (string)input.userName;
            string password = (string)input.password;
            string strNote = (string)input.strNote;
            DateTime expireDate = DateTime.Parse(input.expireDate);
            bool result = await Task.Run(() =>
            {
                return FtpAccount.CreateLocalAccount(userName, password, strNote, expireDate);
            });
            return result;

        }
        
        /// <summary>
        /// Asynchronously delete local account
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
                   
        public async Task <object> DeleteLocalAccountAsync (dynamic input)
        {
            string userName = (string)input;
            bool result = await Task.Run(() =>
            {
                return FtpAccount.DeleteLocalAccount(userName);
            });
            return result;
        }
        /// <summary>
        /// Asynchronously disable or enable local account
        /// If true, enable; false, disable
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
                    
        public async Task<object> EnableLocalAccountAsync(dynamic input)
        {
            string userName = (string)input.userName;
            bool enable = (bool)input.enable;
            bool result = await Task.Run(() =>
            {
                return FtpAccount.EnableLocalAccount(userName, enable);
            });
            return result;
        }

        /// <summary>
        /// Get expire date of local account
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<object> GetExpireDateLocalAccountAsync(dynamic input)
        {
            string userName = (string)input;
            string result = await Task.Run(() =>
            {
                return FtpAccount.GetExpireDateLocalAccount(userName);
            });
            return result;
        }
        /// <summary>
        /// Reset the expire date of local account
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<object> DelayExpireDateLocalAccountAsync(dynamic input)
        {
            string userName = (string)input.userName;
            string expireDate = (string)input.expireDate;
            DateTime dt = DateTime.Parse(expireDate);
            bool result = await Task.Run(() =>
            {
                return FtpAccount.DelayExpireDateLocalAccount(userName, dt);
            });
            return result;
        }
        /// <summary>
        /// Add Virtual User
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<object> AddVirtualUserAsync(dynamic input)
        {
            string siteName = (string)input.siteName;
            string userName = (string)input.userName;
            string physicalPath = (string)input.physicalPath;
            string applicationPool = (string)input.applicationPool;
            bool result = await Task.Run(() =>
            {
                return FtpSiteCtl.AddVirtualUser(siteName, applicationPool, userName, physicalPath);
            });
            return result;
        }

        /// <summary>
        /// Delete Virtual User
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<object> DeleteVirtualUserAsync(dynamic input)
        {
            string siteName = (string)input.siteName;
            string userName = (string)input.userName;
            bool result = await Task.Run(() =>
            {
                return FtpSiteCtl.DeleteVirtualUser(siteName, userName);
            });
            return result;
        }

        /// <summary>
        /// add user permissions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<object> AddUserPermissionAsync(dynamic input)
        {
            string siteName = (string)input.siteName;
            string userName = (string)input.userName;
            string applicationPath = @"/LocalUser/" + userName;
            int permission = (int)input.permission;
            bool result = await Task.Run(() =>
            {
                return FtpSiteCtl.AddUserPermission(siteName, applicationPath, userName, permission);
            });
            return result;
        }

        public async Task<object> addUserAsync(dynamic input)
        {
            string siteName = (string)input.siteName;
            string applicationPool = (string)input.applicationPool;
            string username = (string)input.userName;
            string password = (string)input.password;
            string homePath = (string)input.homeDir;
            DateTime expireDate = DateTime.Parse(input.expireDate);
            string strNote = (string)input.strNote;
            int permission = (int)input.permission;
            bool result = await Task.Run(() =>
            {
                return FtpSiteCtl.addUser(siteName, applicationPool, username, password, homePath, expireDate, strNote, permission);
            });
            return result;
        }

        public async Task<object> removeUserAsync(dynamic input)
        {
            string siteName = (string)input.siteName;
            string username = (string)input.userName;
            bool result = await Task.Run(() =>
            {
                return FtpSiteCtl.removeUser(siteName, username);
            });
            return result;
        }

    }
}
