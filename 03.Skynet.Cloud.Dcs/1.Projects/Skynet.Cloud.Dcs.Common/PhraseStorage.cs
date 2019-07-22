using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using UWay.Skynet.Cloud.Dcs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Common
{
    public class PhraseStorage
    {
        private StringCollection _scOutput = null;	//分词结果保存变量
        private ArrayList _stcOutput = null;		//分词类型结果保存变量

        public PhraseStorage()
        {
            _scOutput = new StringCollection();
            _stcOutput = new ArrayList();
        }
        /// <summary>
        /// 词的数量
        /// </summary>
        public int Length
        {
            get { return _scOutput.Count; }
        }
        /// <summary>
        /// 清除存储的结果
        /// </summary>
        public void ClearResult()
        {
            _scOutput.Clear();
            _stcOutput.Clear();
        }
        /// <summary>
        /// 添加一个词
        /// </summary>
        /// <param name="phrase">词</param>
        public void AddPhrase(string phrase)
        {
            _scOutput.Add(phrase);
        }
        /// <summary>
        /// 添加一个词类
        /// </summary>
        /// <param name="pt">词类</param>
        public void AddPhraseType(PhraseTypeEnum pt)
        {
            _stcOutput.Add(pt);
        }
        /// <summary>
        /// 添加一个词和它对应的词类
        /// </summary>
        /// <param name="phrase">词</param>
        /// <param name="pt">词类</param>
        public void AddPhraseResult(string phrase, PhraseTypeEnum pt)
        {
            _scOutput.Add(phrase);
            _stcOutput.Add(pt);
        }
        /// <summary>
        /// 获得数字的浮点值
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public double GetNumberValue(int index)
        {
            string temp_str = _scOutput[index];
            if (_scOutput[index][0] == '@')
                temp_str = temp_str.Replace('@', '-');	//把'@'转换为负号

            return Convert.ToDouble(temp_str);
        }
        /// <summary>
        /// 输出分词结果
        /// </summary>
        public StringCollection PhraseResult
        {
            get
            {
                return _scOutput;
            }
        }
        /// <summary>
        /// 输出分词类型结果
        /// </summary>
        public PhraseTypeEnum[] PhraseTypeResult
        {
            get
            {
                return (PhraseTypeEnum[])_stcOutput.ToArray(typeof(PhraseTypeEnum));
            }
        }
        private Dictionary<int, PhraseStorage> _SubPhraseStorage = new Dictionary<int, PhraseStorage>();
        public Dictionary<int, PhraseStorage> SubPhraseStorage
        {
            get
            {
                return _SubPhraseStorage;
            }
            set
            {
                _SubPhraseStorage = value;
            }
        }
        /// <summary>
        /// 词法类型表达式字符串
        /// </summary>
        public string ExpressionOutput
        {
            get
            {
                string temp = "|";
                foreach (PhraseTypeEnum item in _stcOutput.ToArray(typeof(PhraseTypeEnum)))
                {
                    temp += ((int)item).ToString() + "|";
                }
                return temp;
            }
        }
    }
}
