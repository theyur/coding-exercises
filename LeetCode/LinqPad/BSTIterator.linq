<Query Kind="Program" />

void Main()
{
}

// You can define other methods, fields, classes and namespaces here

//Definition for a binary tree node.
public class TreeNode {
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}
 
public class BSTIterator
{
	private TreeNode _root;

	private IEnumerable<int> GetNext(TreeNode node)
	{
		if (node.left != null) foreach (var i in GetNext(node.left)) yield return i;
		yield return node.val;
		if (node.right!= null) foreach (var i in GetNext(node.right)) yield return i;
	}

	private IEnumerator<int> _enumerator;
	private int _current;
	private bool _hasNextSet = false;
	private bool _hasNext = false;

	public BSTIterator(TreeNode root)
	{
		_root = root;
		_enumerator = GetNext(_root).GetEnumerator();
	}

	public int Next()
	{
		if (!_hasNextSet)
		{
			_hasNext = _enumerator.MoveNext();
			if (_hasNext) return _enumerator.Current;
		}
		
		_hasNextSet = false;
		return _current;
	}

	public bool HasNext()
	{
		if (_hasNextSet) return _hasNext;
		_hasNext = _enumerator.MoveNext();
		if (_hasNext) _current = _enumerator.Current;
		_hasNextSet = true;
		return _hasNext;
	}
}

/**
 * Your BSTIterator object will be instantiated and called as such:
 * BSTIterator obj = new BSTIterator(root);
 * int param_1 = obj.Next();
 * bool param_2 = obj.HasNext();
 */