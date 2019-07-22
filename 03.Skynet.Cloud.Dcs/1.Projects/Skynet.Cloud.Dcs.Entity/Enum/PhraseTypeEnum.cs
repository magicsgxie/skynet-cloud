using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
   public enum PhraseTypeEnum
    {
        unknown = 0,
        ln = 1,
        lg = 2,
        log = 3,
        greater = 4,		//a^b
        cbrt = 6,		//a^-0.5
        else_ = 7,		//a^-1/3
        equal = 8,
        sin = 10,
        end = 11,
        when = 12,
        case_ = 13,
        tg = 14,
        abs = 15,
        atg = 16,
        then = 17,
        plus = 18,
        minus = 19,
        mutiple = 20,
        divide = 21,
        mod = 23,
        leftbracket = 24,		//(
        rightbracket = 25,	//)
        ans = 26,		//variable ans
        sto = 27,		//save to var
        clr = 28,		//clear vars
        ax = 29,		//variable a
        bx = 30,		//variable b
        cx = 31,		//variable c
        dx = 32,		//variable d
        ex = 33,		//variable e
        fx = 34,		//variable f
        e = 35,
        pi = 36,
        number = 37,
        sharp = 38,
        less = 39,    //负
        positive = 40     //正
    }
}
