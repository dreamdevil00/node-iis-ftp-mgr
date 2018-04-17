using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;

namespace Edge_FtpMgr
{
    public class FtpAccount
    {
        /// <summary>
        /// 创建本地账户
        /// 如果账户已经存在 创建失败
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="strNote">用户描述</param>
        /// <param name="expireDate">过期时间</param>
        /// <returns></returns>
        public static bool CreateLocalAccount(string userName, string password,
            string strNote, DateTime expireDate)
        {
            bool IsSuccess = false;
            if (!IsUserExists(userName))
            {
                try
                {
                    using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
                    {
                        using (UserPrincipal user = new UserPrincipal(context))
                        {
                            user.SetPassword(password);
                            user.Name = userName;
                            user.AccountExpirationDate = expireDate;
                            user.Description = strNote;
                            user.Save();
                            IsSuccess = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return IsSuccess;

        }

        /// <summary>
        /// 删除本地用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool DeleteLocalAccount(string userName)
        {
            bool IsSuccess = false;
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
                {
                    UserPrincipal user =
                        UserPrincipal.FindByIdentity(context, userName);
                    bool isUserExists = user != null;
                    if (isUserExists)
                    {
                        user.Delete();
                    }
                    IsSuccess = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return IsSuccess;
        }

        /// <summary>
        /// 获取本地账户的过期日期
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static string GetExpireDateLocalAccount(string userName)
        {
            string str = "";
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, userName);
                    bool isUserExists = user != null;
                    if (isUserExists)
                    {
                        str = user.AccountExpirationDate.ToString();
                    }
                    
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return str;
        }

        /// <summary>
        /// Delay expired date of the local account with username: userName
        /// </summary>
        /// <param name="userName">The username of the local account</param>
        /// <param name="expireDate">expiredAt date of the account</param>
        /// <returns></returns>
        public static bool DelayExpireDateLocalAccount(string userName, DateTime expireDate)
        {
            bool IsSuccess = false;
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, userName);
                    bool isUserExists = user != null;
                    if (isUserExists)
                    {
                        user.AccountExpirationDate = expireDate;
                        user.Save();
                        IsSuccess = true;
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return IsSuccess;

        }

        /// <summary>
        /// Enable or Disable local account
        /// </summary>
        /// <param name="userName">The usernmae of local account</param>
        /// <param name="enable">Enable if True, otherwise false</param>
        /// <returns></returns>
        public static bool EnableLocalAccount(string userName, bool enable)
        {
            bool IsSuccess = false;
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, userName);
                    bool isUserExists = user != null;
                    if (isUserExists)
                    {
                        user.Enabled = enable;
                        user.Save();
                        IsSuccess = true;
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return IsSuccess;
        }
        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsUserExists(string userName)
        {
            bool found = false;
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, userName);

                    found = user != null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return found;
        }
}
}
