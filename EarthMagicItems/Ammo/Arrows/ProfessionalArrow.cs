﻿using EarthWithMagicAPI.API;
using EarthWithMagicAPI.API.Util;
using System;
using System.Collections.Generic;

namespace DungeonsAndFantasyLands.API.Items.Ammo.Arrows
{
    /// <summary>
    /// A better version of the <see cref="HandMadeArrow"/>
    /// </summary>
    public class ProfessionalArrow : IAmmo
    {
        private int _Uses = Dice.RollDice(1, 3);
        private bool _QuestItem = false;
        private int _Value = 15;
        private int _Level = 1;
        private Guid _ID = new Guid();
        private string _Name = "Professional Arrow";
        private int _ChanceToHit = 13;

        private List<string> _Lore = new List<string> { "Someone or something has made these arrows by hand.", "While these arrows seem handmade, they were obviously made by a skilled arrow maker." };
        private List<string> _OtherInfo = new List<string> { "Does 1d8 piercing damage.", "If you are lucky, you get to use this arrow 3 times." };

        public ProfessionalArrow()
        {

        }

        public Damage AttackDamage
        {
            get
            {
                return new Damage(0, 0, 0, 0, 0, Dice.RollDice(1, 8), 0, 0);
            }
        }

        public int Uses
        {
            get
            {
                return this._Uses;
            }

            set
            {
                this._Uses = value;
            }
        }

        public bool QuestItem
        {
            get
            {
                return this._QuestItem;
            }

            set
            {
                this._QuestItem = value;
            }
        }

        public int Value
        {
            get
            {
                return this._Value;
            }

            set
            {
                this._Value = value;
            }
        }

        public int Level
        {
            get
            {
                return this._Level;
            }

            set
            {
                this._Level = value;
            }
        }

        public Guid ID
        {
            get
            {
                return this._ID;
            }

            set
            {
                this._ID = value;
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                this._Name = value;
            }
        }

        public List<string> Lore
        {
            get
            {
                return this._Lore;
            }

            set
            {
                this._Lore = value;
            }
        }

        public List<string> OtherInformation
        {
            get
            {
                return this._OtherInfo;
            }

            set
            {
                this._OtherInfo = value;
            }
        }

        public int ChanceToHit
        {
            get
            {
                return this._ChanceToHit;
            }

            set
            {
                this._ChanceToHit = value;
            }
        }
    }
}