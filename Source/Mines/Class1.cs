/*
 * Created by SharpDevelop.
 * User: CRC User
 * Date: 7/14/2017
 * Time: 11:25 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Mines
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Class1
	{
		public Class1()
		{
		}
		// Code suggested by http://www.codeproject.com/Members/TheBasketcaseSoftware
public static string TrimAllWithLexerLoop(string s) {
    int length = s.Length;
    var buffer = new StringBuilder(s);
    var dstIdx = 0;
    for (int index = 0; index &lt; s.Length; index++) {
        char ch = s[index];
        switch (ch) {
            case '\u0020': case '\u00A0': case '\u1680': case '\u2000': case '\u2001':
            case '\u2002': case '\u2003': case '\u2004': case '\u2005': case '\u2006':
            case '\u2007': case '\u2008': case '\u2009': case '\u200A': case '\u202F':
            case '\u205F': case '\u3000': case '\u2028': case '\u2029': case '\u0009':
            case '\u000A': case '\u000B': case '\u000C': case '\u000D': case '\u0085':
                length--;
                continue;
            default:
                break;
        }
        buffer[dstIdx++] = ch;
    }
    buffer.Length = length;
    return buffer.ToString();;
}
		
		public static unsafe string TrimAllWithStringInplaceV2(string str) {
    var len = str.Length;
    fixed (char* pStr = str) {
        int dstIdx = 0;
        for (int i = 0; i &lt; len; i++)
            if (!isWhiteSpace(pStr[i]))
                pStr[dstIdx++] = pStr[i];
        // since the unsafe string length reset didn't work we need to resort to this slower compromise
        return new string(pStr, 0, dstIdx);
    }
}
	}
}
