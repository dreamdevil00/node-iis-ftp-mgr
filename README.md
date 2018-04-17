# Description

node-iis-ftp-mgr is a simple module that manages IIS 7.5 or higher FTP account.

Attention: 
In the FTP User isolation configuration, you have to select  ```username directory(global virtual directory)``` below the `isolate user`
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
