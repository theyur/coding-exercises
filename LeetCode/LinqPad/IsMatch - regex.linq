<Query Kind="Program" />

void Main()
{
	Solution solution = new();

	//var r = solution.IsMatch("abcccddbebc", "ab.*..b.bc");
	//var r = solution.IsMatch("ab", ".*");
	//var r = solution.IsMatch("ab", ".*c");
	//var r = solution.IsMatch("aa", "b*");
	//var r = solution.IsMatch("aab", "c*a*b");
	//var r = solution.IsMatch("aaa", "ab*a");
	//var r = solution.IsMatch("aaa", "ab*ac*a");
	//var r = solution.IsMatch("a", "ab*a");
	//var r = solution.IsMatch("a", "ab*c*.*.*f*");
	//var r = solution.IsMatch("a", "ab*a");
	//var r = solution.IsMatch("aaa", "ab*a*c*a");
	//var r = solution.IsMatch("a", ".*.");
	//var r = solution.IsMatch("a", "..*");
	//var r = solution.IsMatch("aaaaaaaaaaaaab", "a*a*a*a*a*a*a*a*a*c*b");
	//var r = solution.IsMatch("a", "..*");
	//var r = solution.IsMatch("a", ".*..a*");
	//var r = solution.IsMatch("acaabbaccbbacaabbbb", "a*.*b*.*a*aa*a*");
	//var r = solution.IsMatch("aaa", "a*a");
	//var r = solution.IsMatch("a", "ab*a");
	//var r = solution.IsMatch("baabbbaccbccacacc", "c*..b*a*a.*a..*c");
	//var r = solution.IsMatch("acbbcbcbcbaaacaac", "ac*.a*ac*.*ab*b*ac");
	//var r = solution.IsMatch("mississippi", "mis*is*ip*.");
	//var r = solution.IsMatch("bbbba", ".*a*a");
	//var r = solution.IsMatch("aabcbcbcaccbcaabc", ".*a*aa*.*b*.c*.*a*");
	var r = solution.IsMatch("abbabaaaaaaacaa", "a*.*b.a.*c*b*a*c*");

	Console.WriteLine(r);
}

// You can define other methods, fields, classes and namespaces here
public class Solution
{
	public bool IsMatch(string s, string p)
	{
		var changed = false;
		do
		{
			changed = false;
			while (p.Contains("**"))
			{
				p = p.Replace("**", "*");
				changed = true;
			}

			for (var i = 0; i < p.Length - 3; i++)
				if (p[i + 1] == '*' && p[i + 3] == '*' && p[i] == p[i + 2])
				{
					p = p.Remove(i, 2);
					changed = true;
				}
		} while (changed);

		s += '!';
		p += '!';

		return IsMatchInternal(s, p, false, 1, 1);
	}
	
	private HashSet<(int, int)> _cache = [];
	
	private bool IsMatchInternal(string s, string p, bool asterisc, int sIdxBase, int iBase)
	{
		if (p == "!" && asterisc) return s == "!";
		
		var sIdx = 0;

		for (var i = 0; i < p.Length; i++)
		{
			switch (p[i])
			{
				case '.' when p[i + 1] == '*':
				case (>= 'a' and <= 'z') when p[i + 1] == '*':
					for (var j = 0; sIdx + j < 21; j++)
					{
						if (_cache.Contains((sIdxBase + sIdx + 1, iBase + j * 10 + i + 2))) return false;
						
						if (IsMatchInternal(s[sIdx..], new string(p[i], j) + p[(i + 2)..], true, sIdxBase + sIdx + 1, iBase + j * 10 + i + 2)) return true;
						else _cache.Add((sIdxBase + sIdx + 1, iBase + j * 10 + i + 2));
					}	
					return false;

				case '.':
					if (s[sIdx] == '!') return false;
					sIdx++;
					break;

				case (>= 'a' and <= 'z'):
					if (s[sIdx] == '!') return false;
					if (s[sIdx] != p[i]) return false;
					sIdx++;
					break;

				case '!':
					return s[sIdx] == '!';

				default:
					throw new Exception("We should not come to here");
			}
		}

		return false;
	}
}


public class Solution1
{
	public bool IsMatch(string s, string p)
	{
		var changed = false;
		do
		{
			changed = false;
			while (p.Contains("**"))
			{
				p = p.Replace("**", "*");
				changed = true;
			}

			for (var i = 1; i < p.Length; i++)
				if (p[i] == '*')
					while (i < p.Length - 1 && p[i - 1] == p[i + 1] && p[i + 1] != '.')
					{
						if (i + 2 <= p.Length - 1 && (p[i + 2] == '*'))
						{
							p = p.Remove(i + 1, 1);
							changed = true;
						}
						else i++;
					}
		} while (changed);
		
		if (p == string.Empty) return true;

		//p = "c*..b*a*.*a..*c";
		p += '!'; s += '!';
		
		return IsMatchInternal(s, p);
	}
	
	private bool IsMatchInternal(string s, string p, bool asterisc = false)
	{
		if ( asterisc && p == "!" && AllCharsAreTheSame(s[..^1])) return true;
		
		if (s == "!" && (p.IndexOf('*') < 0 || p.IndexOf('*') > 2)) return false;
		
		var prev = '!';
		int sIdx = 0, i;
		
		for (i = 0; i < p.Length - 1; i++)
		{
			switch (p[i])
			{
				case '.':
					if (s[sIdx] == '!') 
						if (i >= p.Length - 2 || i == 0 || !(p[i - 1] == '*' || p[i + 1] == '*')) return false;
						else continue;
						
					prev = s[sIdx];
					sIdx++;
					continue;

				case '*':
					var sIdxOld = sIdx;
					if (p[i - 1] != '.')
					{
						while (s[sIdx] == p[i - 1] && s[sIdx] != '!') sIdx++;
						if (s[sIdx] == '!' && p.LastIndexOf('*') == i)
						{
							var sameChars = true;
							for (var j = i + 1; j <= p.Length - 2; j++) sameChars &= p[j] == p[i - 1];
							
							return (sIdx > sIdxOld || p[i - 1] != s[sIdx]) && (i >= p.Length - 2 || sameChars);
						}

						if (sIdx == sIdxOld) continue;
						
						for (var j = sIdxOld - 1; j <= sIdx; j++)
							if (IsMatchInternal(s[j..], p[(i + 1)..], asterisc: true)) return true;
					}
					else
					{
						for (var j = sIdxOld - 1; j >= 0 && j <= s.Length - 1; j++)
							if (IsMatchInternal(s[j..], p[(i + 1)..], asterisc: true)) return true;
					}

					return false;

				case '!':
					return s[sIdx] == '!';

				default:
					if (s[sIdx] != p[i])
						if (p[i + 1] != '*') return false;
						else continue;
						
					prev = s[sIdx];
					sIdx++;
					continue;
			}
		}
		
		return s[sIdx..] == "!";
		
		bool AllCharsAreTheSame(string s)
		{
			var same = true;
			for (var i = 1; i < s.Length; i++) same &= s[i] == s[0];
			return same;
		}
	}
}