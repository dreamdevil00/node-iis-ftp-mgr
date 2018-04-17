'use strict';

const path = require('path');
const edge = require('edge');

const dllPath = path.join(__dirname, './lib/dll/release/Edge_FtpMgr.dll');

const dllLib = {};

/**
 * Method: Create local account as ftp account
 * @param {Object} account
 * @param {string} account.userName ftp username
 * @param {string} account.password ftp password
 * @param {string} account.strNote  ftp account description
 * @param {string} account.expireDate the expired date string of ftp account
 */
dllLib.createLocalAccount = edge.func({
  assemblyFile: dllPath,
  typeName: 'Edge_FtpMgr.Startup',
  methodName: 'CreateLocalAccountAsync'
});

/**
 * Delete Local Account by UserName
 * @param {string} userName the username of local account
 * @return {boolean} true if successfully otherwise false
 */

dllLib.deleteLocalAccount = edge.func({
  assemblyFile: dllPath,
  typeName: 'Edge_FtpMgr.Startup',
  methodName: 'DeleteLocalAccountAsync',
});

/**
 * Disable or Enable Local Account by UserName
 * @param {Object} params
 * @param {string} params.userName the username of local account
 * @param {boolean}params.enable true if enable otherwise false
 * @return {boolean} true if successfully otherwise false
 */

dllLib.enableLocalAccount = edge.func({
  assemblyFile: dllPath,
  typeName: 'Edge_FtpMgr.Startup',
  methodName: 'EnableLocalAccountAsync'
});

/**
 * Get expire date of the account by UserName
 * @param {string} userName the username of local account
 * @return {string} expire date string
 */
dllLib.getExpireDateLocalAccount = edge.func({
  assemblyFile: dllPath,
  typeName: 'Edge_FtpMgr.Startup',
  methodName: 'GetExpireDateLocalAccountAsync'
})

/**
 * Reset expire date of the account by UserName
 * @param {Object} params
 * @param {string} params.userName the username fo the local account
 * @param {string} params.expireDate the expire date
 * @return {boolean} true if success, otherwise false
 */
dllLib.delayExpireDateLocalAccount = edge.func({
  assemblyFile: dllPath,
  typeName: 'Edge_FtpMgr.Startup',
  methodName: 'DelayExpireDateLocalAccountAsync'
})

/**
 * Method: Add virtual user
 * You should supply several parameters
 * @param {Object} options
 * @property {string} siteName the Ftp Site Name
 * @property {string} userName 
 * @property {string} physicalPath
 */

dllLib.addVirtualUser = edge.func({
  assemblyFile: dllPath,
  typeName: 'Edge_FtpMgr.Startup',
  methodName: 'AddVirtualUserAsync'
});

/**
 * Delete Virtual User
 * @param {Object} params
 * @param {string} params.siteName the Ftp site name
 * @param {string} params.userName the local account username
 * @return {boolean} true if success, otherwise false
 */
dllLib.deleteVirtualUser = edge.func({
  assemblyFile: dllPath,
  typeName: 'Edge_FtpMgr.Startup',
  methodName: 'DeleteVirtualUserAsync',
});

/**
 * Add User Permissions
 * @param {Object} Object
 * @param {string} Object.siteName site name
 * @param {string} Object.userName username
 * @param {number} Object.permission 1 for read 2 for RW
 */
dllLib.addUserPermission = edge.func({
  assemblyFile: dllPath,
  typeName: 'Edge_FtpMgr.Startup',
  methodName: 'AddUserPermissionAsync'
});

/**
 * Add User
 * @param {Object} Object
 * @param {string} Object.siteName site name
 * @param {string} Object.applicationPool application pool name
 * @param {string} Object.userName username
 * @param {string} Object.password password
 * @param {string} Object.homeDir home path
 * @param {string} Object.expireDate expire date
 * @param {string} Object.strNote note
 * @param {number} Object.permission 1 readonly 2 read and write
 */
dllLib.addUser = edge.func({
  assemblyFile: dllPath,
  typeName: 'Edge_FtpMgr.Startup',
  methodName: 'addUserAsync'
});

/**
 * remove User
 * @param {Object} Object
 * @param {string} Object.siteName site name
 * @param {string} Object.userName username
 */
dllLib.removeUser = edge.func({
  assemblyFile: dllPath,
  typeName: 'Edge_FtpMgr.Startup',
  methodName: 'removeUserAsync'
});

module.exports = dllLib;
