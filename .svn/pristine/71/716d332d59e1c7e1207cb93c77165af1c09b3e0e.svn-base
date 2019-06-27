using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace UWay.Skynet.Cloud.Upms.Entity
{
    public class PwdConfigInfo
    {
        
        
        public bool? IsCheckUserName
        {
            set;
            get;
        }
        
        
        public bool? IsCheckPwdLength
        {
            set;
            get;
        }
        
        
        public bool? IsCheckPwdCharRepeatRate
        {
            set;
            get;
        }
        
        
        public bool? IsCheckPwdCharRepeatNumber
        {
            set;
            get;
        }
        
        
        public bool? IsCheckPwdMustUpdate
        {
            set;
            get;
        }
        
        
        public int? PwdMinlength
        {
            set;
            get;
        }
        
        
        public int? PwdMaxLength
        {
            set;
            get;
        }
        
        
        public int? PwdCharRepeatRate
        {
            set;
            get;
        }
        
        
        public int? PwdCharRepeatNumber
        {
            set;
            get;
        }
        
        
        public int? PwdUpdateDaysCount
        {
            set;
            get;
        }
        
        
        public DateTime? PwdUpdateSetDate
        {
            set;
            get;
        }
        
        
        public string Mender
        {
            set;
            get;
        }
        
        
        public DateTime? UpdateDate
        {
            set;
            get;
        }
        
        
        public int PwdMix
        {
            set;
            get;
        }
        
        
        public int? PwdHistoryCounter
        {
            set;
            get;
        }
        
        
        public bool IsPwdHistoryCounter
        {
            set;
            get;
        }
        
        
        public int PwdRetrieveType
        {
            set;
            get;
        }
        
        public int PwdRetrievePerday
        {
            set;
            get;
        }

        public bool ValidatePWDComplexity(string sourcePwd, string UserNo, string historyPwd, string encryptPwd, out StringBuilder msg)
        {
            msg = new StringBuilder();
            var result = true;
            if (this.IsCheckPwdLength == true)
            {
                if (sourcePwd.Length < this.PwdMinlength)
                {
                    msg.AppendFormat("密码位数不合法，最低{0}位", PwdMinlength).AppendLine();
                    result = false;
                }
            }

            if (this.IsCheckPwdCharRepeatRate == true)
            {
                int count = 0;
                foreach (char a in sourcePwd)
                {
                    if (UserNo.Contains(a))
                    {
                        count++;
                    }
                }
                if (sourcePwd.Length == 0 ||
                     ((double)count / sourcePwd.Length) * 100 > this.PwdCharRepeatRate)
                {
                    msg.AppendFormat("密码与登录名的重复率不能大于{0}%", PwdCharRepeatRate).AppendLine();
                    result = false;
                }
            }

            if (IsCheckPwdCharRepeatNumber == true)
            {

                foreach (char a in sourcePwd)
                {
                    if (sourcePwd.Count(p => p.Equals(a)) > PwdCharRepeatNumber)
                    {
                        result = false;
                        //result = AuthenType.UserPwdSimple;
                        msg.AppendFormat("密码中任何字符的重复次数不能大于{0}次", PwdCharRepeatNumber).AppendLine();
                        break;
                    }
                }
            }

            if (IsPwdHistoryCounter == true)
            {

                List<string> pwdlist = string.IsNullOrEmpty(historyPwd) ? new List<string>() : historyPwd.Split(',').ToList();
                if (PwdHistoryCounter.HasValue)
                {
                    var count = PwdHistoryCounter.Value;

                    if (pwdlist.Count > count)
                    {
                        pwdlist.RemoveRange(count, pwdlist.Count - count);
                    }

                    if (pwdlist.Contains(encryptPwd))
                    {
                        result = false;
                        msg.AppendFormat("密码不能与最近{0}次密码历史记录重复", count).AppendLine();
                    }
                }

            }

            Regex rgLowerCase = new Regex(@"[a-z]");
            Regex rgUpperCase = new Regex(@"[A-Z]");
            Regex rgNumeric = new Regex(@"[0-9]");
            Regex rgSpecialChar = new Regex(@"[/\|_!@#$%^&*()=+.-]");

            if (PwdMix > 0)
            {
                bool reg = (rgLowerCase.IsMatch(sourcePwd) && rgUpperCase.IsMatch(sourcePwd))
                    && rgNumeric.IsMatch(sourcePwd) && rgSpecialChar.IsMatch(sourcePwd);
                if (!reg)
                {
                    result = false;
                    msg.Append("密码必须以数字字母大小写加特殊符号混合");
                }
            }
            else
            {
                bool reg = (rgLowerCase.IsMatch(sourcePwd) || rgUpperCase.IsMatch(sourcePwd))
                                   && rgNumeric.IsMatch(sourcePwd);
                if (!reg)
                {
                    result = false;
                    msg.Append("密码必须以数字加字符混合");
                }
            }

            return result;
        }

    }
}
