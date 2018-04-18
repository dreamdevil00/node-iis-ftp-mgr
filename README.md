# Description

node-iis-ftp-mgr is a simple module that manages IIS 7.5 or higher FTP account.
Your created user will have it's own home folder which isolate each other's.

Attention:

Before using this lib, you have to do following steps:

- Install IIS7.5 or above for your computer. [Installing-iis-7](https://docs.microsoft.com/en-US/iis/install/installing-iis-7/installing-iis-7-and-above-on-windows-server-2008-or-windows-server-2008-r2)

- Create your own ftp site, for example, named *DemoFtpSite*. [Creating-a-new-ftp-site-in-iis-7](https://docs.microsoft.com/en-US/iis/publish/using-the-ftp-service/creating-a-new-ftp-site-in-iis-7)

- Create an application pool, for example, named *poolName*

- Configure your `FTP User Isolation`,  select  ```User name directory(disable global virtual directories)``` below the `Isolate users`.
[Configuring-ftp-user-isolation-in-iis-7](https://docs.microsoft.com/en-US/iis/publish/using-the-ftp-service/configuring-ftp-user-isolation-in-iis-7)

## How to use?

```
const ftpMgr = require('node-iis-ftp-mgr');
const ONEDAY = 24 * 60 * 60 * 1000;
const DEFAULT_EXPIRE_DAYS = 30 * ONEDAY;
ftpMgr.addUser({
  siteName: 'demoFtpSite',
  userName: 'Demo',
  password: 'Passw0rd',
  applicationPool: 'poolName',
  homeDir: 'D:\\Bob-Home',
  expireDate: '' + new Date(Date.now() + DEFAULT_EXPIRE_DAYS).toLocalString(),
  strNote: 'testFtpAccount',
  permission: 1,
}, function (err, result) {
  if (err) throw err;
  console.log('addUser result: ', result);
})
```

## Methods

- addUser(<object>params, <function>callback) add user to ftp site. `callback` has 2 parameters: <Error>err, <boolean>result. If `result` is true, it's successful, otherwise failed.
  - siteName        - string - ftp site name
  - applicationPool - string - application pool name
  - userName        - string - ftp username
  - password        - string - ftp password
  - homeDir         - string - user home directory
  - expireDate      - string - expired date string
  - strNote         - string - ftp account description
  - permission      - number - 1 for readonly, 2 for read and write

- removeUser(<object>params, <function>callback) - remove user from ftp site. `callback` has 2 parameters: <Error>err, <boolean>result. If `result` is true, it's successful, otherwise failed.
  - siteName        - string - ftp site name
  - userName        - string - username
