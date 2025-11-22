using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template_Advanced_Project.Classes.States;

namespace Template_Advanced_Project.Classes
{


    public class Pokemon
    {
        //FIRE, WATER, ELECTRIC, GRASS, ICE, POISON 
        static int[,] TypeChart = {/*FIRE*/ {50 , 50 , 100 , 200 , 200 , 100},
                                  /*WATER*/ {200, 50 , 100 , 50  , 50  , 100},
                               /*ELECTRIC*/ {100, 200, 50  , 50  , 100 , 100},
                                  /*GRASS*/ {50 , 200, 100 , 50  , 100 , 100},
                                    /*ICE*/ {50 , 50 , 100 , 200 , 50  , 50 },
                                 /*POISON*/ {100, 100, 100 , 200 , 100 , 100}};

        #region Private Fields

        private string name;
        private PokemonType type;    //FIRE, WATER, ELECTRIC, GRASS, ICE, POISON
        private IState status;
        private Move myMove;//Status Attack

        private int hp;      // Hit Points
        private int hp_MAX;  // Max Hit Points
        private int atk;    //Physical Attack Power
        private int atk_Max;//Max Physical Attack Power
        private int def;    //Physical Defense
        private int sp_Atk; //Special Attack Power
        private int sp_Def; //Special Defense
        private int speed;  //Speed
        private int speed_Max;// Max_Speed

        private int catch_Rate;          //Used by Ball to calculate the chance to catching the Pokemon
        private int sleep_Duration = 0; //Used for ASLEEP Pokemon

        private int base_Exp;  //Bonus Question: Used to calculate the reward Point of the Trainer. You can use it for LevelUp also
        private int level = 1; //Bonus Question: Used to calculate the LevelUp
        #endregion

        #region Public Properties

        public string Name { get => name.ToUpper(); }

        public IState state { get => status; }
        public Move MyMove { get => myMove; }

        public int Hp { get => hp; set => hp = value; }
        public int Hp_MAX { get => hp_MAX; }


        public int Catch_Rate { get => catch_Rate; }
        public int StatusThreshold { get => this.status.GetThreshold(); }
        #endregion

        public Pokemon(string name, PokemonType type, int hp, int atk, int def, int sp_Atk, int sp_Def, int speed, int catch_Rate, int base_Exp)
        {
            this.name = name;
            this.type = type;

            //this.status = ActiveState.GetInstance(); //Initialize it by an instance of ActiveState
            this.hp_MAX = hp;//Base Hits Points
            this.hp = 3 * this.hp_MAX;//Hits Points formulas =  3 * HP_MAX (Base HP)
            this.atk = atk;
            this.atk_Max = atk;
            this.def = def / 3;//Devide Def by 3, because Def values are too high
            this.sp_Atk = sp_Atk;
            this.sp_Def = sp_Def / 3;//Devide Sp_Def by 3, because Sp_Def values are too high
            this.speed = speed;
            this.speed_Max = speed;
            this.catch_Rate = catch_Rate;
            this.base_Exp = base_Exp;
            this.status = ActiveState.GetInstance();

        }
        public Pokemon Clone()
        {
            //To clone a pokemon, we have to use HP_MAX (base HP) instead of HP as parameter
            //Because HP = 3 * HP_MAX
            Pokemon clone = new Pokemon(name: this.name,
                                        type: this.type,
                                        hp: this.hp_MAX,
                                        atk: this.atk,
                                        def: this.def,
                                        sp_Atk: this.sp_Atk,
                                        sp_Def: this.sp_Def,
                                        speed: this.speed,
                                        catch_Rate: this.catch_Rate,
                                        base_Exp: this.base_Exp);

            clone.myMove = this.myMove.Clone();
            return clone;
        }

        public bool Is_Fainted()
        {
            return this.hp <= 0;
        }
        //-----------------------------------------------------------------------------------------------
        public void Burn()
        {
            if (this.type != PokemonType.FIRE)
            {
                this.status = BurnedState.GetInstance();
                this.atk = this.atk / 2;   //Attack Power is decreased by 50%
                Console.WriteLine(this.name + " is BURNED !");
            }
        }
        public void Poison()
        {
            if (this.type != PokemonType.POISON)
            {
                this.status = PoisonedState.GetInstance();
                Console.WriteLine(this.name + " is POISONED !");
            }
        }
        public void Freeze()
        {
            if (this.type != PokemonType.ICE && this.type != PokemonType.FIRE)
            {
                //FROZEN Pokemon is unable to use Special Attack
                this.status = FrozenState.GetInstance();
                Console.WriteLine(this.name + " is FROZEN !");
            }
        }
        public void Paralysis()
        {
            if (this.type != PokemonType.ELECTRIC)
            {
                this.status = ParalyzedState.GetInstance();
                this.speed = this.speed / 2; //Speed is decreased by 50%
                Console.WriteLine(this.name + " is PARALYZED !");
            }
        }
        public void Sleep()
        {
            this.status = ASleepState.GetInstance();
            this.sleep_Duration = RNG.GetInstance().Next(1, 8);//Sleep lasts for a randomly chosen duration of 1 to 7 turns
            Console.WriteLine(this.name + " falls ASLEEP !");
        }
        public void Soak()
        {
            this.type = PokemonType.WATER;
            this.myMove = new Move("Soak", 100, 20);
            Console.WriteLine(this.name + " is SOAK and became a WATER Pokemon !");
        }
        //-----------------------------------------------------------------------------------------------
        public void Move_Attack(Pokemon target)
        {
            if (myMove.PP <= 0)
            {
                Console.WriteLine("You cannot perform this move ! PP = 0");
                return;//exit
            }
            //Calculate the chance of hitting the target
            int random = RNG.GetInstance().Next(0, 100);
            if (random < this.myMove.Accuracy)
            {
                switch (this.type)
                {
                    case PokemonType.ELECTRIC:
                        target.Paralysis();
                        myMove.PP--;
                        break;
                    case PokemonType.FIRE:
                        target.Burn();
                        myMove.PP--;
                        break;
                    case PokemonType.ICE:
                        target.Freeze();
                        myMove.PP--;
                        break;
                    case PokemonType.POISON:
                        target.Poison();
                        myMove.PP--;
                        break;
                    case PokemonType.GRASS:
                        target.Sleep();
                        myMove.PP--;
                        break;
                    case PokemonType.WATER:
                        target.Soak();
                        myMove.PP--;
                        break;
                    default:
                        Console.WriteLine("We cannot perform a Move of an Unknow Pokemon Type!");
                        break;
                }
            }
            else
            {
                Console.WriteLine(target.name + " avoided the Status Attack !");
            }
        }
        public void SetState(IState newState)
        {
            status = newState;
        }
        //-----------------------------------------------------------------------------------------------
        public void ApplyPotion(PotionType type)
        {
            switch (type)
            {
                case PotionType.ANTIDOTE:
                    Poison_Heal();
                    break;
                case PotionType.AWAKENING:
                    Sleep_Heal();
                    break;
                case PotionType.BURN_HEAL:
                    Burn_Heal();
                    break;
                case PotionType.FULL_HEAL:
                    Full_Heal();
                    break;
                case PotionType.FULL_RESTORE:
                    Full_Restore();
                    break;
                case PotionType.ICE_HEAL:
                    Ice_Heal();
                    break;
                case PotionType.MAX_POTION:
                    Max_Heal();
                    break;
                case PotionType.PARALYZE_HEAL:
                    Paralyze_Heal();
                    break;
                case PotionType.REVIVE:
                    Revive();
                    break;
                case PotionType.PP_MAX:
                    this.myMove.Resest_PP();
                    break;
                case PotionType.PP_UP:
                    this.myMove.Increase_PP_Max(10);
                    break;
                case PotionType.COURAGE_CANDY:
                    this.sp_Def += 10;
                    break;
                case PotionType.MIGHTY_CANDY:
                    this.atk += 10;
                    this.atk_Max += 10;
                    break;
            }
        }
        //---------------------------------------------------------------------------------------
        public void Burn_Heal()
        {
            if (status is BurnedState)
            {
                this.status = ActiveState.GetInstance();
            }
        }
        public void Poison_Heal()
        {
            if (status is PoisonedState)
            {
                this.status = ActiveState.GetInstance();
            }
        }
        public void Ice_Heal()
        {
            if (status is FrozenState)
            {
                this.status = ActiveState.GetInstance();
            }
        }
        public void Paralyze_Heal()
        {
            if (status is ParalyzedState)
            {
                this.status = ActiveState.GetInstance();
            }
        }
        public void Sleep_Heal()
        {
            if (status is ASleepState)
            {
                this.status = ActiveState.GetInstance();
            }
        }
        public void Max_Heal()
        {
            this.hp = this.hp_MAX;
        }
        public void Full_Heal()
        {
            this.atk = this.atk_Max;
            this.speed = this.speed_Max;

            this.status = ActiveState.GetInstance();



        }
        public void Full_Restore()
        {
            if (!Is_Fainted())
            {
                this.hp = this.hp_MAX;

                this.Full_Heal();
            }
        }
        public void Revive()
        {
            if (Is_Fainted())
            {
                hp = Math.Max(1, hp_MAX / 2);
            }
        }
        public override string ToString()
        {

            return status.ToString();
        }
        public void Take_Damage(int damage, AttackType type)
        {
            int finalDamage = damage;

            // 1. Physical Attack
            if (type == AttackType.PHYSICAL)
            {
                if (damage > this.def)
                {
                    finalDamage = damage - this.def;
                }
                else
                {
                    finalDamage = 0;
                }
            }
            // 2. Special Attack
            else if (type == AttackType.SPECIAL)
            {
                if (damage > this.sp_Def)
                {
                    finalDamage = damage - this.sp_Def;
                }
                else
                {
                    finalDamage = 0;
                }
            }

            // Apply the damage
            this.hp -= finalDamage;

            // 3. Extra damage from Burn or Poison
            if (status is BurnedState || status is PoisonedState)
            {
                status.ExtraDamage(this);
            }

            // Prevent negative HP
            if (this.hp < 0)
                this.hp = 0;
        }


        public void Attack(Pokemon target, AttackType atk_type)
        {
            int random_damage = 0;
            //paralyze
            if (status is ParalyzedState)
            {
                int R = RNG.GetInstance().Next(0, 100);
                if (R < 25)
                {
                    Console.WriteLine(Name + " is paralyzed and can't move!");
                    return; // Skip turn
                }
            }

            if (status is ASleepState)
            {
                if (sleep_Duration > 0)
                {
                    Console.WriteLine(Name + " is a sleep");
                    sleep_Duration--;
                    return;
                }

            }

            if (atk_type == AttackType.STATUS)
            {
                Move_Attack(target);

                return;
            }

            if (atk_type == AttackType.PHYSICAL)
            {
                {
                    random_damage = RNG.GetInstance().Next(0, atk +1);
                }

            }
            
            if(atk_type == AttackType.SPECIAL)
            {

                if (this.status is FrozenState)
                {
                    Console.WriteLine(name + " Cannot use speicla their frozen.");
                    return;
                }
                random_damage = RNG.GetInstance().Next(0, sp_Atk + 1);


            }

            int row = (int)this.type;
            int col = (int)target.type;
            int modifier = TypeChart[row, col];  // Ex: 200 = super effective

            int final_damage = random_damage * modifier / 100;

           
            // TARGET TAKES DAMAGE
       
            target.Take_Damage(final_damage, atk_type);

        }
    }
}