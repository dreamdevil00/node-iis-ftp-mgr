using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.Administration;
using System.IO;

// related document 
// https://msdn.microsoft.com/en-us/library/microsoft.web.administration(v=vs.90).aspx

namespace Edge_FtpMgr
{
    public class FtpSiteCtl
    {
        /// <summary>
        /// 添加虚拟用户
        /// </summary>
        /// <param name="siteName">网站名</param>
        /// <param name="userName">本地帐户用户名</param>
        /// <param name="physicalPath">用户主目录</param>
        /// <returns></returns>
        public static bool AddVirtualUser(string siteName, string applicationPool, string userName, string physicalPath)
        {
            return FtpSiteCtl.AddApplication(siteName, @"/LocalUser/" + userName, applicationPool, "/", physicalPath);
        }
        /// <summary>
        /// 删除Ftp 虚拟用户(删除Ftp 下的虚拟目录)
        /// </summary>
        /// <param name="siteName">网站名</param>
        /// <param name="userName">本地帐户用户名</param>
        /// <returns></returns>
        public static bool DeleteVirtualUser(string siteName, string userName)
        {
            return FtpSiteCtl.RemoveApplication(siteName, @"/LocalUser/" + userName);
        }

        /// <summary>
        /// 添加应用和虚拟目录到网站
        /// </summary>
        /// <param name="siteName">网站名</param>
        /// <param name="applicationPath">应用程序路径</param>
        /// <param name="applicationPool">应用程序池</param>
        /// <param name="virtualDirectoryPath">虚拟目录路径</param>
        /// <param name="physicalPath">虚拟目录物理路径</param>
        /// <returns></returns>
        public static bool AddApplication(string siteName, string applicationPath, string applicationPool, string virtualDirectoryPath, string physicalPath)
        {
            bool isSuccess = false;
            try
            {
                using (ServerManager sm = new ServerManager())
                {
                    Site st = sm.Sites[siteName];
                    Application app = st.Applications.CreateElement();
                    app.Path = applicationPath;
                    app.ApplicationPoolName = applicationPool;

                    VirtualDirectory vdir = app.VirtualDirectories.CreateElement();
                    vdir.Path = virtualDirectoryPath;
                    vdir.PhysicalPath = physicalPath;
                    app.VirtualDirectories.Add(vdir);

                    st.Applications.Add(app);
                    // 如果物理目录不存在 则创建
                    if (!Directory.Exists(physicalPath))
                    {
                        Directory.CreateDirectory(physicalPath);
                    }     

                    sm.CommitChanges();
                    isSuccess = true;
                }
            } catch (Exception e)
            {
                throw e;
            }
            return isSuccess;
        }
        /// <summary>
        /// 从网站中删除应用
        /// </summary>
        /// <param name="siteName">网站名</param>
        /// <param name="applicationPath">应用路径</param>
        /// <returns></returns>
        public static bool RemoveApplication(string siteName, string applicationPath)
        {
            bool isSuccess = false;
            try
            {
                using(ServerManager sm = new ServerManager())
                {
                    Site st = sm.Sites[siteName];
                    Configuration config = sm.GetApplicationHostConfiguration();

                    Application app = null;

                    ApplicationCollection collection = st.Applications;
                    for (int i = 0; i < collection.Count; i++)
                    {
                        if (String.Equals(collection[i].Path, applicationPath))
                        {
                            app = collection[i];
                            break;
                        }
                    }
                    if (app != null)
                    {
                        st.Applications.Remove(app);
                        config.RemoveLocationPath(siteName + applicationPath);

                        sm.CommitChanges();
                    }       
                    isSuccess = true;
                }

            } catch(Exception e)
            {
                throw e;
            }
            return isSuccess;
        }

        /// <summary>
        /// 找到应用程序
        /// </summary>
        /// <param name="siteName">网站名</param>
        /// <param name="applicationPath">应用路径</param>
        /// <returns></returns>
        public static Application FindTheApplication(string siteName, string applicationPath)
        {
            Application app = null;
            try
            {
                using (ServerManager sm = new ServerManager())
                {
                    Site st = sm.Sites[siteName];
                    ApplicationCollection collection = st.Applications;
                    for (int i = 0; i < collection.Count; i++)
                    {
                        if (String.Equals(collection[i].Path, applicationPath))
                        {
                            app = collection[i];
                            break;
                        }
                    }
                }
            }catch (Exception e)
            {
                throw e;
            }
            return app;
        }

        /// <summary>
        /// 添加用户权限
        /// </summary>
        /// <param name="siteName">网站名</param>
        /// <param name="applicationPath">应用路径</param>
        /// <param name="userName">用户名</param>
        /// <param name="permission">权限1 只读 2 读写</param>
        /// <returns></returns>
        public static bool AddUserPermission(string siteName, string applicationPath, string userName, int permission)
        {
            bool isSuccess = false;
            try
            {
                using(ServerManager sm = new ServerManager())
                {
                    Configuration config = sm.GetApplicationHostConfiguration();

                    // Unlock the section
                    ConfigurationSection section = config.GetSection(@"system.ftpServer/security/authorization", siteName + applicationPath);
                    section.OverrideMode = OverrideMode.Allow;
                    sm.CommitChanges();

                    // Get a new instance of the configuration object
                    config = sm.GetApplicationHostConfiguration();
                    section = config.GetSection(@"system.ftpServer/security/authorization", siteName + applicationPath);
                    ConfigurationElementCollection authCollection = section.GetCollection();

                    ConfigurationElement clearElement = authCollection.CreateElement("clear");
                    authCollection.Add(clearElement);

                    ConfigurationElement addElement = authCollection.CreateElement("add");
                    addElement.SetAttributeValue("accessType", "Allow");
                    addElement.SetAttributeValue("users", userName);
                    string per = permission == 1 ? "Read" : "Read, Write";
                    addElement.SetAttributeValue("permissions", per);
                    authCollection.Add(addElement);
                    sm.CommitChanges();
                    isSuccess = true;
                }

            } catch (Exception e)
            {
                throw e;
            }
            return isSuccess;
        }

        public static bool addUser(string siteName, string applicationPool, string username, string password, string homePath, DateTime expireDate, string strNote, int permission)
        {
            bool isSuccess = false;
            try
            {
                bool result  = FtpAccount.CreateLocalAccount(username, password, strNote, expireDate);
                if (result)
                {
                    bool addAppResult = FtpSiteCtl.AddApplication(siteName, @"/LocalUser/" + username, applicationPool, "/", homePath);
                    if (addAppResult)
                    {
                        isSuccess = FtpSiteCtl.AddUserPermission(siteName, @"/LocalUser/" + username, username, permission);
                    }
                }
            } catch (Exception e)
            {
                throw e;
            }
            return isSuccess;
        }

        public static bool removeUser(string siteName, string username)
        {
            bool isSuccess = false;
            try
            {
                bool result = FtpSiteCtl.RemoveApplication(siteName, @"/LocalUser/" + username);
                if (result)
                {
                    isSuccess = FtpAccount.DeleteLocalAccount(username);
                }

            } catch (Exception e)
            {
                throw e;
            }
            return isSuccess;
        }
    }
}
