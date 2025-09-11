namespace hangfire_template.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            /*
            CreateTable(
                "dbo.ttcibd0018888",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TITEM = c.String(name: "T$ITEM"),
                        TDSCA = c.String(name: "T$DSCA"),
                        TSEAK = c.String(name: "T$SEAK"),
                        TSEAB = c.String(name: "T$SEAB"),
                        TEXIN = c.String(name: "T$EXIN"),
                        TIMAG = c.String(name: "T$IMAG"),
                        TCRDT = c.DateTime(name: "T$CRDT"),
                        TLMDT = c.DateTime(name: "T$LMDT"),
                        TCOOD = c.String(name: "T$COOD"),
                        TKITM = c.Decimal(name: "T$KITM", precision: 18, scale: 2),
                        TCITG = c.String(name: "T$CITG"),
                        TITMT = c.Decimal(name: "T$ITMT", precision: 18, scale: 2),
                        TOSYS = c.Decimal(name: "T$OSYS", precision: 18, scale: 2),
                        TCUNI = c.String(name: "T$CUNI"),
                        TUSET = c.String(name: "T$USET"),
                        TLTCT = c.Decimal(name: "T$LTCT", precision: 18, scale: 2),
                        TSERI = c.Decimal(name: "T$SERI", precision: 18, scale: 2),
                        TCNFG = c.Decimal(name: "T$CNFG", precision: 18, scale: 2),
                        TREPL = c.Decimal(name: "T$REPL", precision: 18, scale: 2),
                        TCPRJ = c.String(name: "T$CPRJ"),
                        TCPVA = c.Long(name: "T$CPVA"),
                        TDFIT = c.String(name: "T$DFIT"),
                        TSTOI = c.Int(name: "T$STOI"),
                        TOPTS = c.Int(name: "T$OPTS"),
                        TCUST = c.Int(name: "T$CUST"),
                        TOPOL = c.Int(name: "T$OPOL"),
                        TWPCS = c.Int(name: "T$WPCS"),
                        TCCFG = c.String(name: "T$CCFG"),
                        TUNEF = c.Int(name: "T$UNEF"),
                        TICHG = c.Int(name: "T$ICHG"),
                        TEITM = c.Int(name: "T$EITM"),
                        TUEFS = c.Int(name: "T$UEFS"),
                        TUMER = c.Int(name: "T$UMER"),
                        TCHMA = c.Int(name: "T$CHMA"),
                        TEFCO = c.String(name: "T$EFCO"),
                        TINDT = c.DateTime(name: "T$INDT"),
                        TEDCO = c.Int(name: "T$EDCO"),
                        TMCOA = c.Int(name: "T$MCOA"),
                        TSAYN = c.Int(name: "T$SAYN"),
                        TCONT = c.Int(name: "T$CONT"),
                        TCNTR = c.String(name: "T$CNTR"),
                        TCPCP = c.String(name: "T$CPCP"),
                        TCOEU = c.Int(name: "T$COEU"),
                        TPPEG = c.Int(name: "T$PPEG"),
                        TIPPG = c.Int(name: "T$IPPG"),
                        TPPSS = c.Int(name: "T$PPSS"),
                        TELCM = c.Int(name: "T$ELCM"),
                        TELRQ = c.Int(name: "T$ELRQ"),
                        TDPEG = c.Int(name: "T$DPEG"),
                        TDPTP = c.Int(name: "T$DPTP"),
                        TDPUU = c.Int(name: "T$DPUU"),
                        TSGTC = c.Int(name: "T$SGTC"),
                        TSRCE = c.Int(name: "T$SRCE"),
                        TEFPR = c.Int(name: "T$EFPR"),
                        TDSCB = c.String(name: "T$DSCB"),
                        TDSCC = c.String(name: "T$DSCC"),
                        TDSCD = c.String(name: "T$DSCD"),
                        TWGHT = c.Decimal(name: "T$WGHT", precision: 18, scale: 2),
                        TCWUN = c.String(name: "T$CWUN"),
                        TCTYP = c.String(name: "T$CTYP"),
                        TCPCL = c.String(name: "T$CPCL"),
                        TCPLN = c.String(name: "T$CPLN"),
                        TCMNF = c.String(name: "T$CMNF"),
                        TCSEL = c.String(name: "T$CSEL"),
                        TCSIG = c.String(name: "T$CSIG"),
                        TRDPT = c.String(name: "T$RDPT"),
                        TCTYO = c.String(name: "T$CTYO"),
                        TENVC = c.Int(name: "T$ENVC"),
                        TCEAN = c.String(name: "T$CEAN"),
                        TCCDE = c.String(name: "T$CCDE"),
                        TICSI = c.Int(name: "T$ICSI"),
                        TPSIU = c.Int(name: "T$PSIU"),
                        TSTYP = c.Int(name: "T$STYP"),
                        TSUBC = c.Int(name: "T$SUBC"),
                        TOKTM = c.Long(name: "T$OKTM"),
                        TDPCR = c.Int(name: "T$DPCR"),
                        TTXTA = c.String(name: "T$TXTA"),
                        TCDF_BATP = c.String(name: "T$CDF_BATP"),
                        TREFCNTD = c.Int(name: "T$REFCNTD"),
                        TREFCNTU = c.Int(name: "T$REFCNTU"),
                    })
                .PrimaryKey(t => t.ID);
            */
            
            /*
            CreateTable(
                "dbo.ttdpur2018888",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TRQNO = c.String(name: "T$RQNO"),
                        TPONO = c.Int(name: "T$PONO"),
                        TITEM = c.String(name: "T$ITEM"),
                        TNIDS = c.String(name: "T$NIDS"),
                        TEFFN = c.Long(name: "T$EFFN"),
                        TCRRF = c.Decimal(name: "T$CRRF", precision: 18, scale: 2),
                        TCITT = c.String(name: "T$CITT"),
                        TCRIT = c.String(name: "T$CRIT"),
                        TMPNR = c.String(name: "T$MPNR"),
                        TCMNF = c.String(name: "T$CMNF"),
                        TMITM = c.String(name: "T$MITM"),
                        TQOOR = c.Decimal(name: "T$QOOR", precision: 18, scale: 2),
                        TCUQP = c.String(name: "T$CUQP"),
                        TCVQP = c.Decimal(name: "T$CVQP", precision: 18, scale: 2),
                        TLENG = c.Decimal(name: "T$LENG", precision: 18, scale: 2),
                        TWIDT = c.Decimal(name: "T$WIDT", precision: 18, scale: 2),
                        TTHIC = c.Decimal(name: "T$THIC", precision: 18, scale: 2),
                        TOTBP = c.String(name: "T$OTBP"),
                        TNSDS = c.String(name: "T$NSDS"),
                        TCCON = c.String(name: "T$CCON"),
                        TDLDT = c.DateTime(name: "T$DLDT"),
                        TPRIC = c.Decimal(name: "T$PRIC", precision: 18, scale: 2),
                        TCUPP = c.String(name: "T$CUPP"),
                        TCVPP = c.Decimal(name: "T$CVPP", precision: 18, scale: 2),
                        TOAMT = c.Decimal(name: "T$OAMT", precision: 18, scale: 2),
                        TSITE = c.String(name: "T$SITE"),
                        TCWAR = c.String(name: "T$CWAR"),
                        TCADR = c.String(name: "T$CADR"),
                        TCPRJ = c.String(name: "T$CPRJ"),
                        TCSPA = c.String(name: "T$CSPA"),
                        TCACT = c.String(name: "T$CACT"),
                        TCSTL = c.String(name: "T$CSTL"),
                        TCCCO = c.String(name: "T$CCCO"),
                        TGLCO = c.String(name: "T$GLCO"),
                        TREJC = c.Decimal(name: "T$REJC", precision: 18, scale: 2),
                        TRCOD = c.String(name: "T$RCOD"),
                        TURGT = c.Decimal(name: "T$URGT", precision: 18, scale: 2),
                        TCNTY = c.Decimal(name: "T$CNTY", precision: 18, scale: 2),
                        TBGXC = c.Decimal(name: "T$BGXC", precision: 18, scale: 2),
                        TPEGD = c.Decimal(name: "T$PEGD", precision: 18, scale: 2),
                        TADIN = c.String(name: "T$ADIN"),
                        TCPLA = c.String(name: "T$CPLA"),
                        TTXTA = c.String(name: "T$TXTA"),
                        TREFCNTD = c.Decimal(name: "T$REFCNTD", precision: 18, scale: 2),
                        TREFCNTU = c.Decimal(name: "T$REFCNTU", precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            */
            
            /*
            CreateTable(
                "dbo.ttxmsl4288888",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TORNO = c.String(name: "T$ORNO"),
                        TAPBY = c.String(name: "T$APBY"),
                        TAPDT = c.DateTime(name: "T$APDT"),
                        TFLAG = c.Decimal(name: "T$FLAG", precision: 18, scale: 2),
                        TSTAT = c.Decimal(name: "T$STAT", precision: 18, scale: 2),
                        TREMK = c.String(name: "T$REMK"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ttxmsl4298888",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TORNO = c.String(name: "T$ORNO"),
                        TAPBY = c.String(name: "T$APBY"),
                        TAPDT = c.DateTime(name: "T$APDT"),
                        TFLAG = c.Decimal(name: "T$FLAG", precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            */

            /*
            CreateTable(
                "dbo.twhinh5218888",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TORNO = c.String(name: "T$ORNO"),
                        TPONO = c.Int(name: "T$PONO", nullable: false),
                        TCWAR = c.String(name: "T$CWAR"),
                        TLOCA = c.String(name: "T$LOCA"),
                        TITEM = c.String(name: "T$ITEM"),
                        TCLOT = c.String(name: "T$CLOT"),
                        TIDAT = c.DateTime(name: "T$IDAT"),
                        TSTUN = c.String(name: "T$STUN"),
                        TPKDF = c.String(name: "T$PKDF"),
                        THUID = c.String(name: "T$HUID"),
                        THUPR = c.Decimal(name: "T$HUPR", precision: 18, scale: 2),
                        TQSTP = c.Decimal(name: "T$QSTP", precision: 18, scale: 2),
                        TQSTR = c.Decimal(name: "T$QSTR", precision: 18, scale: 2),
                        TQADJ = c.Decimal(name: "T$QADJ", precision: 18, scale: 2),
                        TQADR = c.Decimal(name: "T$QADR", precision: 18, scale: 2),
                        TQVRC = c.Decimal(name: "T$QVRC", precision: 18, scale: 2),
                        TQVRR = c.Decimal(name: "T$QVRR", precision: 18, scale: 2),
                        TRJIN = c.Decimal(name: "T$RJIN", precision: 18, scale: 2),
                        TADAT = c.DateTime(name: "T$ADAT"),
                        TADRN = c.String(name: "T$ADRN"),
                        TUAPR = c.Decimal(name: "T$UAPR", precision: 18, scale: 2),
                        TADPR = c.Long(name: "T$ADPR"),
                        TPRIC = c.Decimal(name: "T$PRIC", precision: 18, scale: 2),
                        TAMNT = c.Decimal(name: "T$AMNT", precision: 18, scale: 2),
                        TOWNS = c.Decimal(name: "T$OWNS", precision: 18, scale: 2),
                        TOWNR = c.String(name: "T$OWNR"),
                        TISTR = c.Decimal(name: "T$ISTR", precision: 18, scale: 2),
                        TIFBP = c.String(name: "T$IFBP"),
                        TIOWN = c.Decimal(name: "T$IOWN", precision: 18, scale: 2),
                        TDPBY = c.Decimal(name: "T$DPBY", precision: 18, scale: 2),
                        TPRCD = c.Decimal(name: "T$PRCD", precision: 18, scale: 2),
                        TSPCN = c.String(name: "T$SPCN"),
                        TTXTA = c.String(name: "T$TXTA"),
                        TREFCNTD = c.Decimal(name: "T$REFCNTD", precision: 18, scale: 2),
                        TREFCNTU = c.Decimal(name: "T$REFCNTU", precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            */

            /*
            CreateTable(
                "dbo.twhinr1108888",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TITEM = c.String(name: "T$ITEM"),
                        TCWAR = c.String(name: "T$CWAR"),
                        TTRDT = c.DateTime(name: "T$TRDT"),
                        TSEQN = c.Long(name: "T$SEQN"),
                        TSITE = c.String(name: "T$SITE"),
                        TEFFN = c.Long(name: "T$EFFN", nullable: false),
                        TSERL = c.String(name: "T$SERL"),
                        TCLOT = c.String(name: "T$CLOT"),
                        TQSTK = c.Double(name: "T$QSTK"),
                        TOCMP = c.Int(name: "T$OCMP"),
                        TKOOR = c.Decimal(name: "T$KOOR", precision: 18, scale: 2),
                        TKOST = c.Decimal(name: "T$KOST", precision: 18, scale: 2),
                        TITID = c.String(name: "T$ITID"),
                        TITSE = c.Long(name: "T$ITSE"),
                        TORNO = c.String(name: "T$ORNO"),
                        TPONO = c.Int(name: "T$PONO"),
                        TSRNB = c.Int(name: "T$SRNB"),
                        TBOML = c.Int(name: "T$BOML"),
                        TDLIN = c.Int(name: "T$DLIN"),
                        TRCNO = c.String(name: "T$RCNO"),
                        TRCLN = c.Int(name: "T$RCLN"),
                        TPYPS = c.Long(name: "T$PYPS"),
                        TSHPM = c.String(name: "T$SHPM"),
                        TSHPO = c.Int(name: "T$SHPO"),
                        TBPID = c.String(name: "T$BPID"),
                        TCPRJ = c.String(name: "T$CPRJ"),
                        TCSPA = c.String(name: "T$CSPA"),
                        TCACT = c.String(name: "T$CACT"),
                        TCSTL = c.String(name: "T$CSTL"),
                        TCCCO = c.String(name: "T$CCCO"),
                        TPRJP = c.Decimal(name: "T$PRJP", precision: 18, scale: 2),
                        TPRNT = c.Decimal(name: "T$PRNT", nullable: false, precision: 18, scale: 2),
                        TCHAN = c.String(name: "T$CHAN"),
                        TQHND = c.Double(name: "T$QHND"),
                        TLOGN = c.String(name: "T$LOGN"),
                        TCONS = c.Decimal(name: "T$CONS", precision: 18, scale: 2),
                        TOWNS = c.Decimal(name: "T$OWNS", precision: 18, scale: 2),
                        TOWNR = c.String(name: "T$OWNR"),
                        TBFBP = c.String(name: "T$BFBP"),
                        TPHTR = c.Decimal(name: "T$PHTR", precision: 18, scale: 2),
                        TCOSV = c.Decimal(name: "T$COSV", precision: 18, scale: 2),
                        TREJE = c.Decimal(name: "T$REJE", precision: 18, scale: 2),
                        TRECO = c.Decimal(name: "T$RECO", precision: 18, scale: 2),
                        TVALM = c.Decimal(name: "T$VALM", precision: 18, scale: 2),
                        TVWVG = c.Decimal(name: "T$VWVG", precision: 18, scale: 2),
                        TWVGR = c.String(name: "T$WVGR"),
                        TLGDT = c.DateTime(name: "T$LGDT"),
                        TISEQ = c.Int(name: "T$ISEQ"),
                        TTTYP = c.String(name: "T$TTYP"),
                        TCINV = c.Long(name: "T$CINV"),
                        TINVD = c.DateTime(name: "T$INVD"),
                        TITRD = c.DateTime(name: "T$ITRD"),
                        TILGD = c.DateTime(name: "T$ILGD"),
                        TSPCN = c.String(name: "T$SPCN"),
                        TREFCNTD = c.Decimal(name: "T$REFCNTD", precision: 18, scale: 2),
                        TREFCNTU = c.Decimal(name: "T$REFCNTU", precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            */
            
            /*
            CreateTable(
                "dbo.twhwmd2158888",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TCWAR = c.String(name: "T$CWAR"),
                        TITEM = c.String(name: "T$ITEM"),
                        TQHND = c.Double(name: "T$QHND"),
                        TQCHD = c.Double(name: "T$QCHD"),
                        TQNHD = c.Double(name: "T$QNHD"),
                        TQBLK = c.Double(name: "T$QBLK"),
                        TQBPL = c.Double(name: "T$QBPL"),
                        TQORD = c.Double(name: "T$QORD"),
                        TQOOR = c.Double(name: "T$QOOR"),
                        TQCOR = c.Double(name: "T$QCOR"),
                        TQNOR = c.Double(name: "T$QNOR"),
                        TQINT = c.Double(name: "T$QINT"),
                        TQCIT = c.Double(name: "T$QCIT"),
                        TQNIT = c.Double(name: "T$QNIT"),
                        TQALL = c.Double(name: "T$QALL"),
                        TQOAL = c.Double(name: "T$QOAL"),
                        TQCAL = c.Double(name: "T$QCAL"),
                        TQNAL = c.Double(name: "T$QNAL"),
                        TQNBL = c.Double(name: "T$QNBL"),
                        TQNBP = c.Double(name: "T$QNBP"),
                        TQCOM = c.Double(name: "T$QCOM"),
                        TQLAL = c.Double(name: "T$QLAL"),
                        TQCPR = c.Double(name: "T$QCPR"),
                        TQHRJ = c.Double(name: "T$QHRJ"),
                        TQCRJ = c.Double(name: "T$QCRJ"),
                        TQNRJ = c.Double(name: "T$QNRJ"),
                        TLTDT = c.DateTime(name: "T$LTDT"),
                        THSTD = c.DateTime(name: "T$HSTD"),
                        TLSID = c.DateTime(name: "T$LSID"),
                        TQCIS = c.Double(name: "T$QCIS"),
                        TQHIB = c.Double(name: "T$QHIB"),
                    })
                .PrimaryKey(t => t.ID);
            */

            /*
            CreateTable(
                "dbo.ttdpur2008888",
                c => new
                    {
                        INSERT_DATE = c.DateTime(nullable: false),
                        T_RQNO = c.String(),
                        T_ORIG = c.Decimal(precision: 18, scale: 2),
                        T_REMN = c.String(),
                        T_RDEP = c.String(),
                        T_COFC = c.String(),
                        T_RDAT = c.DateTime(),
                        T_AEMN = c.String(),
                        T_ADEP = c.String(),
                        T_LTDT = c.DateTime(),
                        T_RQST = c.Decimal(precision: 18, scale: 2),
                        T_CONV = c.Decimal(precision: 18, scale: 2),
                        T_SITE = c.String(),
                        T_CWAR = c.String(),
                        T_DADR = c.String(),
                        T_CPRJ = c.String(),
                        T_CSPA = c.String(),
                        T_CACT = c.String(),
                        T_CSTL = c.String(),
                        T_CCCO = c.String(),
                        T_DLDT = c.DateTime(),
                        T_REFA = c.String(),
                        T_REFB = c.String(),
                        T_LOGN = c.String(),
                        T_CCUR = c.String(),
                        T_CCON = c.String(),
                        T_URGT = c.Decimal(precision: 18, scale: 2),
                        T_CNTY = c.Decimal(precision: 18, scale: 2),
                        T_SPAP = c.Decimal(precision: 18, scale: 2),
                        T_RCOD = c.String(),
                        T_ADIN = c.String(),
                        T_CCTY = c.String(),
                        T_TXTA = c.Decimal(precision: 18, scale: 2),
                        T_CDF_DESC = c.String(),
                        T_CDF_LEVL = c.String(),
                        T_CDF_RJMR = c.String(),
                        T_CDF_TGLB = c.DateTime(),
                        T_CDF_WRFL = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTD = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTU = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.INSERT_DATE);
            */

            /*
            CreateTable(
                "dbo.ttdpur2028888",
                c => new
                    {
                        INSERT_DATE = c.DateTime(nullable: false),
                        T_RQNO = c.String(),
                        T_PONO = c.Decimal(precision: 18, scale: 2),
                        T_PDNO = c.String(),
                        T_RSIT = c.String(),
                        T_MNIT = c.String(),
                        T_OPRO = c.String(),
                        T_ROUC = c.String(),
                        T_RORV = c.String(),
                        T_OPNO = c.Decimal(precision: 18, scale: 2),
                        T_OPSQ = c.Decimal(precision: 18, scale: 2),
                        T_PCLN = c.Decimal(precision: 18, scale: 2),
                        T_SRVO = c.String(),
                        T_SRPO = c.Decimal(precision: 18, scale: 2),
                        T_MNWO = c.String(),
                        T_MNLP = c.Decimal(precision: 18, scale: 2),
                        T_PRNO = c.String(),
                        T_PPON = c.Decimal(precision: 18, scale: 2),
                        T_SQNB = c.Decimal(precision: 18, scale: 2),
                        T_QONO = c.String(),
                        T_QPON = c.Decimal(precision: 18, scale: 2),
                        T_QSEQ = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTD = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTU = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.INSERT_DATE);
            */

            /*
            CreateTable(
                "dbo.ttdpur4008888",
                c => new
                    {
                        INSERT_DATE = c.DateTime(nullable: false),
                        T_ORNO = c.String(),
                        T_OTBP = c.String(),
                        T_OTAD = c.String(),
                        T_OTCN = c.String(),
                        T_SFBP = c.String(),
                        T_SFAD = c.String(),
                        T_SFCN = c.String(),
                        T_IFBP = c.String(),
                        T_IFAD = c.String(),
                        T_IFCN = c.String(),
                        T_PTBP = c.String(),
                        T_PTAD = c.String(),
                        T_PTCN = c.String(),
                        T_CORG = c.Decimal(precision: 18, scale: 2),
                        T_COTP = c.String(),
                        T_RAGR = c.String(),
                        T_CPAY = c.String(),
                        T_ODAT = c.DateTime(),
                        T_ODIS = c.Decimal(precision: 18, scale: 2),
                        T_CCUR = c.String(),
                        T_MCFR = c.Decimal(precision: 18, scale: 2),
                        T_RATP_1 = c.Decimal(precision: 18, scale: 2),
                        T_RATP_2 = c.Decimal(precision: 18, scale: 2),
                        T_RATP_3 = c.Decimal(precision: 18, scale: 2),
                        T_RATF_1 = c.Decimal(precision: 18, scale: 2),
                        T_RATF_2 = c.Decimal(precision: 18, scale: 2),
                        T_RATF_3 = c.Decimal(precision: 18, scale: 2),
                        T_RATD = c.DateTime(),
                        T_RATT = c.String(),
                        T_RAUR = c.Decimal(precision: 18, scale: 2),
                        T_SITE = c.String(),
                        T_CWAR = c.String(),
                        T_CADR = c.String(),
                        T_CCON = c.String(),
                        T_PLNR = c.String(),
                        T_CCRS = c.String(),
                        T_CFRW = c.String(),
                        T_CPLP = c.String(),
                        T_BPPR = c.String(),
                        T_BPTX = c.String(),
                        T_CDEC = c.String(),
                        T_PTPA = c.String(),
                        T_DDAT = c.DateTime(),
                        T_DDTC = c.DateTime(),
                        T_CBRN = c.String(),
                        T_CREG = c.String(),
                        T_REFA = c.String(),
                        T_REFB = c.String(),
                        T_PRNO = c.String(),
                        T_CTRJ = c.String(),
                        T_COFC = c.String(),
                        T_FDPT = c.String(),
                        T_ODTY = c.Decimal(precision: 18, scale: 2),
                        T_ODNO = c.String(),
                        T_RETR = c.String(),
                        T_SORN = c.String(),
                        T_COSN = c.String(),
                        T_AKCD = c.String(),
                        T_CRCD = c.String(),
                        T_CTCD = c.String(),
                        T_EGEN = c.Decimal(precision: 18, scale: 2),
                        T_SBIM = c.Decimal(precision: 18, scale: 2),
                        T_PAFT = c.Decimal(precision: 18, scale: 2),
                        T_SBMT = c.String(),
                        T_BPCL = c.String(),
                        T_OAMT = c.Decimal(precision: 18, scale: 2),
                        T_COMM = c.Decimal(precision: 18, scale: 2),
                        T_IEBP = c.Decimal(precision: 18, scale: 2),
                        T_IAFC = c.Decimal(precision: 18, scale: 2),
                        T_LCCL = c.String(),
                        T_HDST = c.Decimal(precision: 18, scale: 2),
                        T_HISP = c.Decimal(precision: 18, scale: 2),
                        T_HISM = c.Decimal(precision: 18, scale: 2),
                        T_ADIN = c.String(),
                        T_CHRQ = c.Decimal(precision: 18, scale: 2),
                        T_REVN = c.Decimal(precision: 18, scale: 2),
                        T_OPOR = c.String(),
                        T_CRBY = c.String(),
                        T_CRDT = c.DateTime(),
                        T_CROR = c.Decimal(precision: 18, scale: 2),
                        T_CRCL = c.Decimal(precision: 18, scale: 2),
                        T_CRIN = c.Decimal(precision: 18, scale: 2),
                        T_CRRQ = c.Decimal(precision: 18, scale: 2),
                        T_LCRQ = c.Decimal(precision: 18, scale: 2),
                        T_ETPC = c.Decimal(precision: 18, scale: 2),
                        T_CCTY = c.String(),
                        T_CVYN = c.Decimal(precision: 18, scale: 2),
                        T_TXTA = c.Decimal(precision: 18, scale: 2),
                        T_TXTB = c.Decimal(precision: 18, scale: 2),
                        T_CRHT = c.Decimal(precision: 18, scale: 2),
                        T_CDF_DESC = c.String(),
                        T_CDF_LEVL = c.String(),
                        T_CDF_RJMR = c.String(),
                        T_CDF_RQNO = c.String(),
                        T_CDF_WRFL = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTD = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTU = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.INSERT_DATE);
            */

            /*
            CreateTable(
                "dbo.ttdpur4028888",
                c => new
                    {
                        INSERT_DATE = c.DateTime(nullable: false),
                        T_ORNO = c.String(),
                        T_PONO = c.Decimal(precision: 18, scale: 2),
                        T_SQNB = c.Decimal(precision: 18, scale: 2),
                        T_CORG = c.Decimal(precision: 18, scale: 2),
                        T_OORN = c.String(),
                        T_OPON = c.Decimal(precision: 18, scale: 2),
                        T_OSQN = c.Decimal(precision: 18, scale: 2),
                        T_SORN = c.String(),
                        T_SPON = c.Decimal(precision: 18, scale: 2),
                        T_SSEQ = c.Decimal(precision: 18, scale: 2),
                        T_SCSQ = c.Decimal(precision: 18, scale: 2),
                        T_PDNO = c.String(),
                        T_OPNO = c.Decimal(precision: 18, scale: 2),
                        T_PPON = c.Decimal(precision: 18, scale: 2),
                        T_LSTN = c.String(),
                        T_SRVO = c.String(),
                        T_SRPO = c.Decimal(precision: 18, scale: 2),
                        T_SRSQ = c.Decimal(precision: 18, scale: 2),
                        T_QONO = c.String(),
                        T_QPON = c.Decimal(precision: 18, scale: 2),
                        T_QALT = c.Decimal(precision: 18, scale: 2),
                        T_QSEQ = c.Decimal(precision: 18, scale: 2),
                        T_FONO = c.String(),
                        T_FOPO = c.Decimal(precision: 18, scale: 2),
                        T_RQNO = c.String(),
                        T_RQPO = c.Decimal(precision: 18, scale: 2),
                        T_ODTY = c.Decimal(precision: 18, scale: 2),
                        T_ODNO = c.String(),
                        T_RETO = c.String(),
                        T_RETP = c.Decimal(precision: 18, scale: 2),
                        T_RETS = c.Decimal(precision: 18, scale: 2),
                        T_RERS = c.Decimal(precision: 18, scale: 2),
                        T_RTYN = c.Decimal(precision: 18, scale: 2),
                        T_MNWO = c.String(),
                        T_MNLP = c.Decimal(precision: 18, scale: 2),
                        T_MNSQ = c.Decimal(precision: 18, scale: 2),
                        T_INVC = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTD = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTU = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.INSERT_DATE);
            */

            /*
            CreateTable(
                "dbo.ttfbgc1208888",
                c => new
                    {
                        INSERT_DATE = c.DateTime(nullable: false),
                        T_ACNT = c.String(),
                        T_DIM1 = c.String(),
                        T_DIM2 = c.String(),
                        T_DIM3 = c.String(),
                        T_DIM4 = c.String(),
                        T_DIM5 = c.String(),
                        T_DIM6 = c.String(),
                        T_DIM7 = c.String(),
                        T_DIM8 = c.String(),
                        T_DIM9 = c.String(),
                        T_DM10 = c.String(),
                        T_DM11 = c.String(),
                        T_DM12 = c.String(),
                        T_DIMS = c.String(),
                        T_DSC1 = c.String(),
                        T_DSC2 = c.String(),
                        T_CABG = c.Decimal(precision: 18, scale: 2),
                        T_LTDB = c.Decimal(precision: 18, scale: 2),
                        T_CHKA = c.Decimal(precision: 18, scale: 2),
                        T_EXBG = c.Decimal(precision: 18, scale: 2),
                        T_TAMT = c.Decimal(precision: 18, scale: 2),
                        T_TCUR = c.String(),
                        T_TPRC = c.Decimal(precision: 18, scale: 2),
                        T_DCMP = c.Decimal(precision: 18, scale: 2),
                        T_CWOC = c.String(),
                        T_BMGR = c.String(),
                        T_LEVL = c.Decimal(precision: 18, scale: 2),
                        T_SORT = c.Decimal(precision: 18, scale: 2),
                        T = c.String(),
                        T_REFCNTD = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTU = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.INSERT_DATE);
            */

            /*
            CreateTable(
                "dbo.ttfbgc1608888",
                c => new
                    {
                        INSERT_DATE = c.DateTime(nullable: false),
                        T_YEAR = c.Decimal(precision: 18, scale: 2),
                        T_BDGT = c.String(),
                        T_LEVL = c.Decimal(precision: 18, scale: 2),
                        T_ACNT = c.String(),
                        T_DIMS = c.String(),
                        T_PERD = c.Decimal(precision: 18, scale: 2),
                        T_BGAM = c.Decimal(precision: 18, scale: 2),
                        T_ALAM = c.Decimal(precision: 18, scale: 2),
                        T_RLAM = c.Decimal(precision: 18, scale: 2),
                        T_PAAM = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTD = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTU = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.INSERT_DATE);
            
            CreateTable(
                "dbo.ttfbgc4008888",
                c => new
                    {
                        INSERT_DATE = c.DateTime(nullable: false),
                        T = c.DateTime(),
                        T_SEQN = c.Decimal(precision: 18, scale: 2),
                        T_DCMP = c.Decimal(precision: 18, scale: 2),
                        T_DCIT = c.Decimal(precision: 18, scale: 2),
                        T_DCMT = c.String(),
                        T_DCLN = c.String(),
                        T_DCRF = c.String(),
                        T_YEAR = c.Decimal(precision: 18, scale: 2),
                        T_BDGT = c.String(),
                        T_LEVL = c.Decimal(precision: 18, scale: 2),
                        T_DSQN = c.Decimal(precision: 18, scale: 2),
                        T_DCST = c.Decimal(precision: 18, scale: 2),
                        T_TRTP = c.Decimal(precision: 18, scale: 2),
                        T_RDCP = c.Decimal(precision: 18, scale: 2),
                        T_RDIT = c.Decimal(precision: 18, scale: 2),
                        T_RDCM = c.String(),
                        T_RDLN = c.String(),
                        T_RDRF = c.String(),
                        T_RDST = c.Decimal(precision: 18, scale: 2),
                        T_ACNT = c.String(),
                        T_DIMS = c.String(),
                        T_DIM1 = c.String(),
                        T_DIM2 = c.String(),
                        T_DIM3 = c.String(),
                        T_DIM4 = c.String(),
                        T_DIM5 = c.String(),
                        T_DIM6 = c.String(),
                        T_DIM7 = c.String(),
                        T_DIM8 = c.String(),
                        T_DIM9 = c.String(),
                        T_DM10 = c.String(),
                        T_DM11 = c.String(),
                        T_DM12 = c.String(),
                        T_AMNT = c.Decimal(precision: 18, scale: 2),
                        T_CCUR = c.String(),
                        T_DBCR = c.Decimal(precision: 18, scale: 2),
                        T_AMBC = c.Decimal(precision: 18, scale: 2),
                        T_RBAM = c.Decimal(precision: 18, scale: 2),
                        T_BCUR = c.String(),
                        T_QNTY = c.Decimal(precision: 18, scale: 2),
                        T_RQTY = c.Decimal(precision: 18, scale: 2),
                        T_UOMS = c.String(),
                        T_UPRC = c.Decimal(precision: 18, scale: 2),
                        T_EFFD = c.DateTime(),
                        T_TTAM = c.Decimal(precision: 18, scale: 2),
                        T_NRTA = c.Decimal(precision: 18, scale: 2),
                        T_BPID = c.String(),
                        T_ITEM = c.String(),
                        T_PERD = c.Decimal(precision: 18, scale: 2),
                        T_BABT = c.Decimal(precision: 18, scale: 2),
                        T_BAAT = c.Decimal(precision: 18, scale: 2),
                        T_USER = c.String(),
                        T_RTYP = c.String(),
                        T_RTDT = c.DateTime(),
                        T_RATE = c.Decimal(precision: 18, scale: 2),
                        T_BLTP = c.Decimal(precision: 18, scale: 2),
                        T_BUYR = c.String(),
                        T_ENTP = c.Decimal(precision: 18, scale: 2),
                        T_ENID = c.String(),
                        T_TETP = c.Decimal(precision: 18, scale: 2),
                        T_TEID = c.String(),
                        T_DACN = c.String(),
                        T_DDM1 = c.String(),
                        T_DDM2 = c.String(),
                        T_DDM3 = c.String(),
                        T_DDM4 = c.String(),
                        T_DDM5 = c.String(),
                        T_DDM6 = c.String(),
                        T_DDM7 = c.String(),
                        T_DDM8 = c.String(),
                        T_DDM9 = c.String(),
                        T_DD10 = c.String(),
                        T_DD11 = c.String(),
                        T_DD12 = c.String(),
                        T_TRST = c.Decimal(precision: 18, scale: 2),
                        T_RSNC = c.String(),
                        T_EXCP = c.Decimal(precision: 18, scale: 2),
                        T_DAMT = c.Decimal(precision: 18, scale: 2),
                        T_DAIT = c.Decimal(precision: 18, scale: 2),
                        T_LAMN = c.Decimal(precision: 18, scale: 2),
                        T_LTTA = c.Decimal(precision: 18, scale: 2),
                        T_LNRT = c.Decimal(precision: 18, scale: 2),
                        T_DQTY = c.Decimal(precision: 18, scale: 2),
                        T_DUOM = c.String(),
                        T_LQTY = c.Decimal(precision: 18, scale: 2),
                        T_DCDT = c.DateTime(),
                        T_DRTP = c.String(),
                        T_FTRT = c.String(),
                        T_DDCR = c.Decimal(precision: 18, scale: 2),
                        T_REIN = c.Decimal(precision: 18, scale: 2),
                        T_RERE = c.Decimal(precision: 18, scale: 2),
                        T_PCRE = c.Decimal(precision: 18, scale: 2),
                        T_FREC = c.Decimal(precision: 18, scale: 2),
                        T_FSHI = c.Decimal(precision: 18, scale: 2),
                        T_RECO = c.Decimal(precision: 18, scale: 2),
                        T_FRCO = c.Decimal(precision: 18, scale: 2),
                        T_INRE = c.Decimal(precision: 18, scale: 2),
                        T_CRNO = c.Decimal(precision: 18, scale: 2),
                        T_AUTR = c.Decimal(precision: 18, scale: 2),
                        T_CHNG = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTD = c.Decimal(precision: 18, scale: 2),
                        T_REFCNTU = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.INSERT_DATE);
            */


            CreateTable(
                "dbo.t_checklist_item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChecklistId = c.Int(nullable: false),
                        OpenProjectItemId = c.String(),
                        TrelloItemId = c.String(),
                        Content = c.String(),
                        IsDone = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.t_checklist", t => t.ChecklistId, cascadeDelete: true)
                .Index(t => t.ChecklistId);
            
            CreateTable(
                "dbo.t_checklist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkPackageId = c.Int(nullable: false),
                        OpenProjectChecklistId = c.String(),
                        TrelloChecklistId = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.t_work_package", t => t.WorkPackageId, cascadeDelete: true)
                .Index(t => t.WorkPackageId);
            
            /*
            CreateTable(
                "dbo.t_work_package",
                c => new
                    {
                        id_work_package = c.Int(nullable: false, identity: true),
                        OpenProjectWorkPackageId = c.String(),
                        TrelloCardId = c.String(),
                        ProjectId = c.Int(),
                        StatusId = c.Int(),
                        AssigneeId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        DueDate = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                        LastSyncedAt = c.DateTime(nullable: false),
                        NeedsOpSync = c.Boolean(nullable: false),
                        NeedsTrelloSync = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_work_package)
                .ForeignKey("dbo.t_user", t => t.AssigneeId)
                .ForeignKey("dbo.t_project", t => t.ProjectId)
                .ForeignKey("dbo.t_status", t => t.StatusId)
                .Index(t => t.OpenProjectWorkPackageId, unique: true, name: "IX_WorkPackageId")
                .Index(t => t.TrelloCardId, unique: true)
                .Index(t => t.ProjectId)
                .Index(t => t.StatusId)
                .Index(t => t.AssigneeId);
            */
            
            CreateTable(
                "dbo.t_user",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OpenProjectUserId = c.String(),
                        TrelloMemberId = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.t_comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkPackageId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        OpenProjectActivityId = c.String(),
                        TrelloActionId = c.String(),
                        Content = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.t_user", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.t_work_package", t => t.WorkPackageId, cascadeDelete: true)
                .Index(t => t.WorkPackageId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.t_project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OpenProjectIdentifier = c.String(),
                        TrelloBoardId = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.t_status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OpenProjectStatusId = c.String(),
                        TrelloListId = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            /*
            CreateTable(
                "dbo.temp_dashboard_linechart_sales_order",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        year = c.Int(),
                        month_sort = c.Int(),
                        month_desc = c.String(),
                        delivered_uninvoice = c.Decimal(precision: 18, scale: 3),
                        delivered_invoiced = c.Decimal(precision: 18, scale: 3),
                        undelivered = c.Decimal(precision: 18, scale: 3),
                        cancel_so = c.Decimal(precision: 18, scale: 3),
                        avg_value = c.Decimal(precision: 18, scale: 3),
                    })
                .PrimaryKey(t => t.id);
            */

            /*
            CreateTable(
                "dbo.TempListDataRejectPortals",
                c => new
                    {
                        prno = c.Long(nullable: false, identity: true),
                        popo = c.Long(),
                        item = c.String(),
                        mitm = c.String(),
                        rdat = c.DateTime(),
                        qoor = c.Decimal(precision: 18, scale: 2),
                        cwar = c.String(),
                        cwoc = c.String(),
                        crdt = c.DateTime(),
                    })
                .PrimaryKey(t => t.prno);
            */

            /*
            CreateTable(
                "dbo.tlkp_user_mapping",
                c => new
                    {
                        mapping_id = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        trello_user_id = c.String(),
                        openproject_user_id = c.String(),
                    })
                .PrimaryKey(t => t.mapping_id);
            
            CreateTable(
                "dbo.t_sync_log",
                c => new
                    {
                        id_sync_log = c.Int(nullable: false, identity: true),
                        source = c.String(),
                        email = c.String(),
                        card_id = c.String(),
                        checklist_id = c.String(),
                        checklist_item_id = c.String(),
                        time_entry_id = c.String(),
                        work_package_id = c.String(),
                        activity_id = c.String(),
                        synced_at = c.DateTime(),
                        direction = c.String(),
                        sync_status = c.String(),
                        error_message = c.String(),
                    })
                .PrimaryKey(t => t.id_sync_log);
            */

        }

        public override void Down()
        {
            DropForeignKey("dbo.t_work_package", "StatusId", "dbo.t_status");
            DropForeignKey("dbo.t_work_package", "ProjectId", "dbo.t_project");
            DropForeignKey("dbo.t_comment", "WorkPackageId", "dbo.t_work_package");
            DropForeignKey("dbo.t_comment", "AuthorId", "dbo.t_user");
            DropForeignKey("dbo.t_checklist", "WorkPackageId", "dbo.t_work_package");
            DropForeignKey("dbo.t_work_package", "AssigneeId", "dbo.t_user");
            DropForeignKey("dbo.t_checklist_item", "ChecklistId", "dbo.t_checklist");
            DropIndex("dbo.t_comment", new[] { "AuthorId" });
            DropIndex("dbo.t_comment", new[] { "WorkPackageId" });
            DropIndex("dbo.t_work_package", new[] { "AssigneeId" });
            DropIndex("dbo.t_work_package", new[] { "StatusId" });
            DropIndex("dbo.t_work_package", new[] { "ProjectId" });
            DropIndex("dbo.t_work_package", new[] { "TrelloCardId" });
            DropIndex("dbo.t_work_package", "IX_WorkPackageId");
            DropIndex("dbo.t_checklist", new[] { "WorkPackageId" });
            DropIndex("dbo.t_checklist_item", new[] { "ChecklistId" });
            DropTable("dbo.t_sync_log");
            DropTable("dbo.tlkp_user_mapping");
            DropTable("dbo.TempListDataRejectPortals");
            DropTable("dbo.temp_dashboard_linechart_sales_order");
            DropTable("dbo.t_status");
            DropTable("dbo.t_project");
            DropTable("dbo.t_comment");
            DropTable("dbo.t_user");
            DropTable("dbo.t_work_package");
            DropTable("dbo.t_checklist");
            DropTable("dbo.t_checklist_item");
            DropTable("dbo.ttfbgc4008888");
            DropTable("dbo.ttfbgc1608888");
            DropTable("dbo.ttfbgc1208888");
            DropTable("dbo.ttdpur4028888");
            DropTable("dbo.ttdpur4008888");
            DropTable("dbo.ttdpur2028888");
            DropTable("dbo.ttdpur2008888");
            DropTable("dbo.twhwmd2158888");
            DropTable("dbo.twhinr1108888");
            DropTable("dbo.twhinh5218888");
            DropTable("dbo.ttxmsl4298888");
            DropTable("dbo.ttxmsl4288888");
            DropTable("dbo.ttdpur2018888");
            DropTable("dbo.ttcibd0018888");
        }
    }
}
