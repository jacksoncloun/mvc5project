using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    public delegate void rcar(string str);
    public class ClassReturn
    {
        public event rcar _rcar;
        public ClassReturn()
        {
            //eat("西红柿");
            //eat("土豆丝");
            Food food = cook("西红柿");
            food.print();
            Food food2 = cook("土豆丝");
            food2.print();
        }

        public void eat(string name)
        {
            Food food;
            if (name.Equals("西红柿")) food =new Food1();
            else if (name.Equals("土豆丝")) food = new Food2();
        }



        public static Food cook(string type)
        {
            Food food = null;
            if (type.Equals("西红柿"))
            {
                food = new Food1();
            }
            else if (type.Equals("土豆丝"))
            {
                food = new Food2();
            }
            return food;
        }

    }
    public abstract class Food 
    {
        public abstract void print();
    }
    public class Food1 : Food
    {
        public override void print()
        {
            Console.Write("吃西红柿");
        }
    }
    public class Food2 : Food
    {
        public override void print()
        {
            Console.Write("吃青椒土豆丝");
        }
    }

}
