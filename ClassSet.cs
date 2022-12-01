using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace lab1
{
    public class UserSet
    {
        protected List<int> elementsValue = new List<int>();

        public List<int> elements
        {
            get
            {
                elementsValue.Sort();
                return elementsValue;
            }
            set
            {
                elementsValue = value;
            }
        }

        public UserSet()
        {
            elements = new List<int>();
        }

        public UserSet(in int[] elems)
        {
            foreach (int i in elems)
            {
                Add(i);
            }
        }

        public UserSet(in UserSet toCopy)
        {
            foreach (int i in toCopy.elements)
            {
                Add(i);
            }
        }

        public void Add(in int elem)
        {
            if (!elements.Contains(elem))
            {
                elements.Add(elem);
            }
        }

        public void Add(in UserSet set)
        {
            foreach (int i in set.elements)
            {
                Add(i);
            }
        }

        public void Add(in int[] elems)
        {
            foreach (int i in elems)
            {
                Add(i);
            }
        }

        public void Add(in List<int> elems)
        {
            foreach (int i in elems)
            {
                Add(i);
            }
        }

        public void Clear()
        {
            elements.Clear();
        }

        public int Size()
        {
            return elements.Count();
        }        

        override public string ToString()
        {
            StringBuilder ans = new StringBuilder();
            ans.Append("{ ");
            if (Size() > 0)
            {
                foreach (int i in elements)
                {
                    ans.Append(i + " ");
                }
            }
            else
            {
                ans.Append("- ");
            }
            ans.Append("}");
            return ans.ToString();
        }

        public void Print()
        {
            Console.Write(this.ToString());
        }

        public bool Contains(in int elem)
        {
            return elements.Contains(elem);
        }

        public bool Contains(in List<int> arr)
        {
            foreach(int i in arr)
            {
                if (!Contains(i))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Contains(in UserSet set)
        {
            return Contains(set.elements);
        }

        public int Rand()
        {
            if (Size() != 0)
            {
                var rand = new Random();
                return elements[rand.Next() % Size()];
            }
            return 0;
        }

        public UserSet IntersectWith(in UserSet set)
        {
            var ans = new UserSet();
            foreach (int i in elements)
            {
                if (set.Contains(i))
                {
                    ans.Add(i);
                }
            }
            return ans;
        }

        public UserSet UnionWith(in UserSet set)
        {
            var ans = new UserSet(this);
            foreach (int i in set.elements)
            {
                ans.Add(i);
            }
            return ans;
        }

        public UserSet ExceptWith(in UserSet set)
        {
            var ans = new UserSet();
            foreach (int i in elements)
            {
                if (!set.Contains(i))
                {
                    ans.Add(i);
                }
            }
            return ans;
        }

        public UserSet SymmetricExceptWith(in UserSet set)
        {
            var ans = new UserSet();            
            ans.Add(this.ExceptWith(set));
            ans.Add(set.ExceptWith(this));
            return ans;
        }
    }
}