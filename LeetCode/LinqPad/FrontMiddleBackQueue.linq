<Query Kind="Program" />

void Main()
{
	FrontMiddleBackQueue q = new();
	
	List<int> r = [];

	/*	
	q.PushFront(888438);
	q.PushMiddle(772690);
	q.PushMiddle(375192);
	q.PushFront(411268);
	q.PushFront(885613);
	q.PushMiddle(508187);
	
	r.Add(q.PopMiddle());
	r.Add(q.PopMiddle());
	//r.Add(q.PopMiddle());
	//r.Add(q.PopMiddle());
	//r.Add(q.PopMiddle());
	q.PushMiddle(111498);
	q.PushMiddle(296008);
	r.Add(q.PopFront());
*/

	q.PushFront(1);
	q.PushBack(2);
	q.PushMiddle(3);
	q.PushMiddle(4);
	
	r.Add(q.PopFront());
	r.Add(q.PopMiddle());
	r.Add(q.PopMiddle());
	r.Add(q.PopBack());
	r.Add(q.PopFront());
	
	Debugger.Break();
}

// You can define other methods, fields, classes and namespaces here
public class FrontMiddleBackQueue
{
	SortedList<double, int> _sl = [];
	
	public FrontMiddleBackQueue() {}

	public void PushFront(int val)
	{
		if (_sl.Count == 0)
		{
			_sl.Add(0, val);
			return;
		}
		
		var r = _sl.First();
		_sl.Add(r.Key - 1, val);
	}

	public void PushMiddle(int val)
	{
		if (_sl.Count == 0)
		{
			_sl.Add(0, val);
			return;
		}
		
		if (_sl.Count == 1)
		{
			var r1 = _sl.First();
			_sl.Add(r1.Key - 1, val);
			return;
		}
		
		var idx = _sl.Count / 2;
		var p = _sl.Skip(idx - 1);
		var r = p.Take(2).ToList();
		
		if (Math.Abs(r[1].Key - r[0].Key) < 0.00000001)
		{
			SortedList<double, int> sl = [];
			var i = -(_sl.Count / 2);
			foreach (var e in _sl) sl.Add(i++, e.Value);
			_sl = sl;
			PushMiddle(val);
			return;
		}

		var newIdx = (r[0].Key + r[1].Key) / 2.0;
		_sl.Add(newIdx, val);
	}

	public void PushBack(int val)
	{
		if (_sl.Count == 0)
		{
			_sl.Add(0, val);
			return;
		}

		var r = _sl.Last();
		_sl.Add(r.Key + 1, val);
	}

	public int PopFront()
	{
		if (_sl.Count == 0) return -1;
		
		var r = _sl.First();
		_sl.Remove(r.Key);
		return r.Value;
	}

	public int PopMiddle()
	{
		if (_sl.Count == 0) return -1;
		
		var idx = _sl.Count % 2 == 0 ? _sl.Count / 2 : _sl.Count / 2 + 1;
		var p = _sl.Skip(idx - 1);
		var r = p.First();
		_sl.Remove(r.Key);
		return r.Value;
	}

	public int PopBack()
	{
		if (_sl.Count == 0) return -1;
		
		var r = _sl.Last();
		_sl.Remove(r.Key);
		return r.Value;
	}
}
