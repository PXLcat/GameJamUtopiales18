﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJamUtopiales
{
    public interface ICollidable
    {
        Rectangle HitBox { get;}
        void OnCollision(ICollidable other);
    }
}

