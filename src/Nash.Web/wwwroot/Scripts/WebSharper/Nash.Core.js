(function(Global)
{
 "use strict";
 var Nash,Models,User,AccountType,Account,IntelliFactory,Runtime;
 Nash=Global.Nash=Global.Nash||{};
 Models=Nash.Models=Nash.Models||{};
 User=Models.User=Models.User||{};
 AccountType=Models.AccountType=Models.AccountType||{};
 Account=Models.Account=Models.Account||{};
 IntelliFactory=Global.IntelliFactory;
 Runtime=IntelliFactory&&IntelliFactory.Runtime;
 User=Models.User=Runtime.Class({
  toString:function()
  {
   return this.Name;
  },
  get_Create:function()
  {
   return User.New("",null);
  }
 },null,User);
 User.New=function(Name,LastLoggedIn)
 {
  return new User({
   Name:Name,
   LastLoggedIn:LastLoggedIn
  });
 };
 AccountType.OTHER={
  $:4
 };
 AccountType.CASH={
  $:3
 };
 AccountType.EXPENSE={
  $:2
 };
 AccountType.INCOME={
  $:1
 };
 AccountType.BANK={
  $:0
 };
 AccountType.Parse=function(s)
 {
  return s.toUpperCase()==="BANK"?AccountType.BANK:AccountType.OTHER;
 };
 Account.New=function(Guid,Name,Type,ParentGuid,Description)
 {
  return{
   Guid:Guid,
   Name:Name,
   Type:Type,
   ParentGuid:ParentGuid,
   Description:Description
  };
 };
}(self));
