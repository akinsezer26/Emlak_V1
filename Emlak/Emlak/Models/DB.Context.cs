﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emlak.Models
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
    
        public virtual DbSet<AJANDA> AJANDA { get; set; }
        public virtual DbSet<ALACAKLARIM> ALACAKLARIM { get; set; }
        public virtual DbSet<ARSA_TARLA> ARSA_TARLA { get; set; }
        public virtual DbSet<BANYO_SAYISI> BANYO_SAYISI { get; set; }
        public virtual DbSet<BINA> BINA { get; set; }
        public virtual DbSet<BINA_YASI> BINA_YASI { get; set; }
        public virtual DbSet<BORCLARIM> BORCLARIM { get; set; }
        public virtual DbSet<BorcluTakip> BorcluTakip { get; set; }
        public virtual DbSet<BULUNDUGU_KAT> BULUNDUGU_KAT { get; set; }
        public virtual DbSet<EmlakArayanlar> EmlakArayanlar { get; set; }
        public virtual DbSet<EMLAKTURU> EMLAKTURU { get; set; }
        public virtual DbSet<GABARI> GABARI { get; set; }
        public virtual DbSet<IMAR_DURUMU> IMAR_DURUMU { get; set; }
        public virtual DbSet<ISITMA_TIPI> ISITMA_TIPI { get; set; }
        public virtual DbSet<ISYERITURU> ISYERITURU { get; set; }
        public virtual DbSet<KAKS> KAKS { get; set; }
        public virtual DbSet<KAT_SAYISI> KAT_SAYISI { get; set; }
        public virtual DbSet<KiraTakip> KiraTakip { get; set; }
        public virtual DbSet<KontratTakip> KontratTakip { get; set; }
        public virtual DbSet<KONUT_ISYERI> KONUT_ISYERI { get; set; }
        public virtual DbSet<KULLANIM_DURUMU> KULLANIM_DURUMU { get; set; }
        public virtual DbSet<MAKBUZ_SOZLESME> MAKBUZ_SOZLESME { get; set; }
        public virtual DbSet<MESAJ> MESAJ { get; set; }
        public virtual DbSet<ODA_SAYISI> ODA_SAYISI { get; set; }
        public virtual DbSet<TAPU_DURUMU> TAPU_DURUMU { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }
        public virtual DbSet<USERTYPES> USERTYPES { get; set; }
    }
}
