﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


namespace Web.Models
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class Entities : DbContext
{
    public Entities()
        : base("name=Entities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<TG_Templet> TG_Templet { get; set; }

    public virtual DbSet<TG_WebSite> TG_WebSite { get; set; }

    public virtual DbSet<TG_User> TG_User { get; set; }

    public virtual DbSet<TG_Category> TG_Category { get; set; }

    public virtual DbSet<TG_Article> TG_Article { get; set; }

    public virtual DbSet<TG_Client> TG_Client { get; set; }

}

}

