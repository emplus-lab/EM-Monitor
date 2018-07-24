using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;

namespace UserModule_SNMP_CLIENT_PROCESSOR_MODULE_V1_1
{
    public class UserModuleClass_SNMP_CLIENT_PROCESSOR_MODULE_V1_1 : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput CONNECT;
        Crestron.Logos.SplusObjects.DigitalInput DISCONNECT;
        Crestron.Logos.SplusObjects.BufferInput FROM_MODULES;
        Crestron.Logos.SplusObjects.DigitalOutput IS_COMMUNICATING;
        Crestron.Logos.SplusObjects.DigitalOutput IS_INITIALIZED;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> TO_MODULES;
        SplusUdpSocket SNMPAGENT;
        UShortParameter PROTOCOL;
        StringParameter COMMUNITY;
        StringParameter IPADDRESS;
        _MODULECOMM [] MODULECOMM;
        _COMMQUEUE QUEUECOMM;
        CrestronString [] SCOMMANDQUEUE;
        CrestronString [] SSTATUSQUEUE;
        _SNMPMSG PARSEDSNMPMSG;
        _SNMPMSG_PDU PARSEDSNMPMSG_PDU;
        _SNMPMSG_VARBIND PARSEDSNMPMSG_VARBIND;
        _SNMPMSGITEM PARSEDSNMPMSGITEM;
        ushort PARSINGDEVICEBUSY = 0;
        CrestronString PARSEDDEVICEMSG;
        ushort RESPONSEMSGID = 0;
        ushort PARSINGMODULEBUSY = 0;
        CrestronString PARSEDMODULEMSG;
        CrestronString PARSEDMODULETYPE;
        CrestronString PARSEDMODULEOID;
        CrestronString PARSEDMODULEVALUE;
        CrestronString TRASH;
        ushort RESPONSEMODULEMSGID = 0;
        ushort PARSESTATUS = 0;
        ushort INITMODULEID = 0;
        ushort REGMODULEID = 0;
        ushort ISINITIALIZING = 0;
        ushort ISREGISTERING = 0;
        ushort ISHEARTBEATING = 0;
        ushort ISCONNECTED = 0;
        private CrestronString PRINTBYTES (  SplusExecutionContext __context__, CrestronString VALUE ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5000, this );
            
            ushort I = 0;
            
            
            __context__.SourceCodeLine = 229;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)Functions.Length( VALUE ); 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 231;
                MakeString ( RESPONSE , "{0} 0x{1:x2}", RESPONSE , Byte( VALUE , (int)( I ) )) ; 
                __context__.SourceCodeLine = 229;
                } 
            
            __context__.SourceCodeLine = 234;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString PRINTDECBYTES (  SplusExecutionContext __context__, CrestronString VALUE ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5000, this );
            
            ushort I = 0;
            
            
            __context__.SourceCodeLine = 242;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)Functions.Length( VALUE ); 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 244;
                MakeString ( RESPONSE , "{0} {1:d}", RESPONSE , (short)Byte( VALUE , (int)( I ) )) ; 
                __context__.SourceCodeLine = 242;
                } 
            
            __context__.SourceCodeLine = 247;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString GETBOUNDSTRING (  SplusExecutionContext __context__, CrestronString SOURCE , CrestronString STARTSTRING , CrestronString ENDSTRING ) 
            { 
            ushort STARTINDEX = 0;
            
            ushort ENDINDEX = 0;
            
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 256, this );
            
            
            __context__.SourceCodeLine = 259;
            RESPONSE  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 261;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( SOURCE ) > 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 263;
                STARTINDEX = (ushort) ( Functions.Find( STARTSTRING , SOURCE ) ) ; 
                __context__.SourceCodeLine = 264;
                ENDINDEX = (ushort) ( Functions.Find( ENDSTRING , SOURCE , (STARTINDEX + 1) ) ) ; 
                __context__.SourceCodeLine = 266;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( STARTINDEX > 0 ) ) && Functions.TestForTrue ( Functions.BoolToInt ( STARTINDEX < ENDINDEX ) )) ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 268;
                    STARTINDEX = (ushort) ( (STARTINDEX + Functions.Length( STARTSTRING )) ) ; 
                    __context__.SourceCodeLine = 270;
                    RESPONSE  .UpdateValue ( Functions.Mid ( SOURCE ,  (int) ( STARTINDEX ) ,  (int) ( (ENDINDEX - STARTINDEX) ) )  ) ; 
                    } 
                
                } 
            
            __context__.SourceCodeLine = 274;
            return ( RESPONSE ) ; 
            
            }
            
        private void INITQUEUE (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 282;
            QUEUECOMM . NBUSY = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 283;
            QUEUECOMM . NHASITEMS = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 284;
            QUEUECOMM . NCOMMANDHEAD = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 285;
            QUEUECOMM . NCOMMANDTAIL = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 286;
            QUEUECOMM . NSTATUSHEAD = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 287;
            QUEUECOMM . NSTATUSTAIL = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 288;
            QUEUECOMM . NSTRIKECOUNT = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 289;
            QUEUECOMM . NRESENDLAST = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 290;
            QUEUECOMM . SLASTMESSAGE  .UpdateValue ( ""  ) ; 
            
            }
            
        private void INITMODULES (  SplusExecutionContext __context__ ) 
            { 
            ushort ID = 0;
            
            
            __context__.SourceCodeLine = 297;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ISINITIALIZING == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 299;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( INITMODULEID ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)Functions.GetNumArrayRows( TO_MODULES ); 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( ID  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (ID  >= __FN_FORSTART_VAL__1) && (ID  <= __FN_FOREND_VAL__1) ) : ( (ID  <= __FN_FORSTART_VAL__1) && (ID  >= __FN_FOREND_VAL__1) ) ; ID  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 301;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( MODULECOMM[ ID ].ISREGISTERED ) && Functions.TestForTrue ( Functions.BoolToInt (MODULECOMM[ ID ].ISINITIALIZED == 0) )) ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 303;
                        ISINITIALIZING = (ushort) ( 1 ) ; 
                        __context__.SourceCodeLine = 305;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (IsSignalDefined( TO_MODULES[ ID ] ) == 1))  ) ) 
                            {
                            __context__.SourceCodeLine = 306;
                            MakeString ( TO_MODULES [ ID] , "INIT<{0:d}>", (short)ID) ; 
                            }
                        
                        __context__.SourceCodeLine = 308;
                        INITMODULEID = (ushort) ( ID ) ; 
                        __context__.SourceCodeLine = 310;
                        break ; 
                        } 
                    
                    __context__.SourceCodeLine = 313;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (ID == Functions.GetNumArrayRows( TO_MODULES )) ) && Functions.TestForTrue ( Functions.BoolToInt (ISINITIALIZING == 0) )) ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 315;
                        INITMODULEID = (ushort) ( ID ) ; 
                        __context__.SourceCodeLine = 316;
                        IS_INITIALIZED  .Value = (ushort) ( 1 ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 299;
                    } 
                
                } 
            
            
            }
            
        private void REINIT (  SplusExecutionContext __context__ ) 
            { 
            ushort ID = 0;
            
            
            __context__.SourceCodeLine = 326;
            INITMODULEID = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 327;
            ISINITIALIZING = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 328;
            IS_INITIALIZED  .Value = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 330;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)Functions.GetNumArrayCols( MODULECOMM ); 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( ID  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (ID  >= __FN_FORSTART_VAL__1) && (ID  <= __FN_FOREND_VAL__1) ) : ( (ID  <= __FN_FORSTART_VAL__1) && (ID  >= __FN_FOREND_VAL__1) ) ; ID  += (ushort)__FN_FORSTEP_VAL__1) 
                {
                __context__.SourceCodeLine = 331;
                MODULECOMM [ ID] . ISINITIALIZED = (ushort) ( 0 ) ; 
                __context__.SourceCodeLine = 330;
                }
            
            
            }
            
        private void RESET (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 336;
            REINIT (  __context__  ) ; 
            __context__.SourceCodeLine = 337;
            INITQUEUE (  __context__  ) ; 
            
            }
            
        private void SOCKETSENDPACKET (  SplusExecutionContext __context__, CrestronString PACKET ) 
            { 
            
            __context__.SourceCodeLine = 345;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ISCONNECTED == 1))  ) ) 
                {
                __context__.SourceCodeLine = 346;
                Functions.SocketSend ( SNMPAGENT , PACKET ) ; 
                }
            
            
            }
            
        private ushort STARTSWITH (  SplusExecutionContext __context__, CrestronString MATCH_STRING , CrestronString SOURCE_STRING ) 
            { 
            
            __context__.SourceCodeLine = 354;
            return (ushort)( Functions.BoolToInt (Functions.Find( MATCH_STRING , SOURCE_STRING ) == 1)) ; 
            
            }
            
        private ushort CONTAINS (  SplusExecutionContext __context__, CrestronString MATCH_STRING , CrestronString SOURCE_STRING ) 
            { 
            
            __context__.SourceCodeLine = 359;
            return (ushort)( Functions.BoolToInt ( Functions.Find( MATCH_STRING , SOURCE_STRING ) > 0 )) ; 
            
            }
            
        private uint ATOL_SIGNED (  SplusExecutionContext __context__, CrestronString VALUE ) 
            { 
            
            __context__.SourceCodeLine = 364;
            if ( Functions.TestForTrue  ( ( STARTSWITH( __context__ , "-" , VALUE ))  ) ) 
                {
                __context__.SourceCodeLine = 365;
                return (uint)( (0 - Functions.Atol( VALUE ))) ; 
                }
            
            else 
                {
                __context__.SourceCodeLine = 367;
                return (uint)( Functions.Atol( VALUE )) ; 
                }
            
            
            return 0; // default return value (none specified in module)
            }
            
        private CrestronString ENCODE_SNMPLENGTH (  SplusExecutionContext __context__, ushort LENGTH ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 6, this );
            
            CrestronString ENCODING;
            ENCODING  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 4, this );
            
            
            __context__.SourceCodeLine = 378;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( LENGTH < 128 ))  ) ) 
                { 
                __context__.SourceCodeLine = 380;
                MakeString ( RESPONSE , "{0}", Functions.Chr (  (int) ( LENGTH ) ) ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 384;
                do 
                    { 
                    __context__.SourceCodeLine = 386;
                    MakeString ( ENCODING , "{0}{1}", Functions.Chr (  (int) ( Functions.Low( (ushort) LENGTH ) ) ) , ENCODING ) ; 
                    __context__.SourceCodeLine = 388;
                    LENGTH = (ushort) ( (LENGTH >> 8) ) ; 
                    } 
                while (false == ( Functions.TestForTrue  ( Functions.BoolToInt (LENGTH == 0)) )); 
                __context__.SourceCodeLine = 392;
                MakeString ( RESPONSE , "{0}{1}", Functions.Chr (  (int) ( (128 | Functions.Length( ENCODING )) ) ) , ENCODING ) ; 
                } 
            
            __context__.SourceCodeLine = 395;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPINTEGER (  SplusExecutionContext __context__, int VALUE ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 6, this );
            
            CrestronString ENCODING;
            ENCODING  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 4, this );
            
            uint TEMP = 0;
            
            
            __context__.SourceCodeLine = 405;
            TEMP = (uint) ( VALUE ) ; 
            __context__.SourceCodeLine = 407;
            ENCODING  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 409;
            do 
                { 
                __context__.SourceCodeLine = 411;
                MakeString ( ENCODING , "{0}{1}", Functions.Chr (  (int) ( Functions.Low( (ushort) TEMP ) ) ) , ENCODING ) ; 
                __context__.SourceCodeLine = 413;
                TEMP = (uint) ( (TEMP >> 8) ) ; 
                } 
            while (false == ( Functions.TestForTrue  ( Functions.BoolToInt (TEMP == 0)) )); 
            __context__.SourceCodeLine = 417;
            MakeString ( RESPONSE , "{0}{1}{2}", Functions.Chr (  (int) ( 2 ) ) , ENCODE_SNMPLENGTH (  __context__ , (ushort)( Functions.Length( ENCODING ) )) , ENCODING ) ; 
            __context__.SourceCodeLine = 419;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPOCTETSTRING (  SplusExecutionContext __context__, CrestronString VALUE ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 132, this );
            
            
            __context__.SourceCodeLine = 426;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( VALUE ) > 128 ))  ) ) 
                {
                __context__.SourceCodeLine = 427;
                MakeString ( RESPONSE , "{0}{1}{2}", Functions.Chr (  (int) ( 4 ) ) , ENCODE_SNMPLENGTH (  __context__ , (ushort)( 128 )) , Functions.Left ( VALUE ,  (int) ( 128 ) ) ) ; 
                }
            
            else 
                {
                __context__.SourceCodeLine = 429;
                MakeString ( RESPONSE , "{0}{1}{2}", Functions.Chr (  (int) ( 4 ) ) , ENCODE_SNMPLENGTH (  __context__ , (ushort)( Functions.Length( VALUE ) )) , VALUE ) ; 
                }
            
            __context__.SourceCodeLine = 431;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPNULL (  SplusExecutionContext __context__ ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 2, this );
            
            
            __context__.SourceCodeLine = 438;
            MakeString ( RESPONSE , "\u0005\u0000") ; 
            __context__.SourceCodeLine = 440;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPOID_VALUE (  SplusExecutionContext __context__, ushort VALUE ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 6, this );
            
            ushort HIGHBYTE = 0;
            
            
            __context__.SourceCodeLine = 448;
            HIGHBYTE = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 449;
            RESPONSE  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 451;
            do 
                { 
                __context__.SourceCodeLine = 453;
                MakeString ( RESPONSE , "{0}{1}", Functions.Chr (  (int) ( (HIGHBYTE | Mod( VALUE , 128 )) ) ) , RESPONSE ) ; 
                __context__.SourceCodeLine = 455;
                HIGHBYTE = (ushort) ( 128 ) ; 
                __context__.SourceCodeLine = 457;
                VALUE = (ushort) ( (VALUE / 128) ) ; 
                } 
            while (false == ( Functions.TestForTrue  ( Functions.BoolToInt (VALUE == 0)) )); 
            __context__.SourceCodeLine = 461;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPOID (  SplusExecutionContext __context__, CrestronString OID ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 132, this );
            
            CrestronString ENCODED;
            ENCODED  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 128, this );
            
            CrestronString TRASH;
            TRASH  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
            
            ushort VALUE = 0;
            
            
            __context__.SourceCodeLine = 471;
            RESPONSE  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 472;
            ENCODED  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 474;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Length( OID ) == 1))  ) ) 
                { 
                __context__.SourceCodeLine = 476;
                MakeString ( ENCODED , "{0}", Functions.Chr (  (int) ( 40 ) ) ) ; 
                __context__.SourceCodeLine = 477;
                OID  .UpdateValue ( ""  ) ; 
                } 
            
            else 
                {
                __context__.SourceCodeLine = 479;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Find( "1.3." , OID ) == 1))  ) ) 
                    { 
                    __context__.SourceCodeLine = 481;
                    MakeString ( ENCODED , "{0}", Functions.Chr (  (int) ( 43 ) ) ) ; 
                    __context__.SourceCodeLine = 483;
                    TRASH  .UpdateValue ( Functions.Remove ( "1.3." , OID )  ) ; 
                    __context__.SourceCodeLine = 485;
                    while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( OID ) > 0 ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 487;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "." , OID ) > 0 ))  ) ) 
                            {
                            __context__.SourceCodeLine = 488;
                            VALUE = (ushort) ( Functions.Atoi( Functions.Remove( "." , OID ) ) ) ; 
                            }
                        
                        else 
                            { 
                            __context__.SourceCodeLine = 491;
                            VALUE = (ushort) ( Functions.Atoi( OID ) ) ; 
                            __context__.SourceCodeLine = 492;
                            OID  .UpdateValue ( ""  ) ; 
                            } 
                        
                        __context__.SourceCodeLine = 495;
                        MakeString ( ENCODED , "{0}{1}", ENCODED , ENCODE_SNMPOID_VALUE (  __context__ , (ushort)( VALUE )) ) ; 
                        __context__.SourceCodeLine = 485;
                        } 
                    
                    } 
                
                }
            
            __context__.SourceCodeLine = 499;
            MakeString ( RESPONSE , "{0}{1}{2}", Functions.Chr (  (int) ( 6 ) ) , ENCODE_SNMPLENGTH (  __context__ , (ushort)( Functions.Length( ENCODED ) )) , ENCODED ) ; 
            __context__.SourceCodeLine = 501;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPVARBIND_INTEGER (  SplusExecutionContext __context__, CrestronString OID , int VALUE ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 260, this );
            
            CrestronString ENCODED;
            ENCODED  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 256, this );
            
            
            __context__.SourceCodeLine = 509;
            MakeString ( ENCODED , "{0}{1}", ENCODE_SNMPOID (  __context__ , OID) , ENCODE_SNMPINTEGER (  __context__ , (int)( VALUE )) ) ; 
            __context__.SourceCodeLine = 511;
            MakeString ( RESPONSE , "{0}{1}{2}", Functions.Chr (  (int) ( 48 ) ) , ENCODE_SNMPLENGTH (  __context__ , (ushort)( Functions.Length( ENCODED ) )) , ENCODED ) ; 
            __context__.SourceCodeLine = 513;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPVARBIND_STRING (  SplusExecutionContext __context__, CrestronString OID , CrestronString VALUE ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 260, this );
            
            CrestronString ENCODED;
            ENCODED  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 256, this );
            
            
            __context__.SourceCodeLine = 521;
            MakeString ( ENCODED , "{0}{1}", ENCODE_SNMPOID (  __context__ , OID) , ENCODE_SNMPOCTETSTRING (  __context__ , VALUE) ) ; 
            __context__.SourceCodeLine = 523;
            MakeString ( RESPONSE , "{0}{1}{2}", Functions.Chr (  (int) ( 48 ) ) , ENCODE_SNMPLENGTH (  __context__ , (ushort)( Functions.Length( ENCODED ) )) , ENCODED ) ; 
            __context__.SourceCodeLine = 525;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPVARBIND_NULL (  SplusExecutionContext __context__, CrestronString OID ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 260, this );
            
            CrestronString ENCODED;
            ENCODED  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 256, this );
            
            
            __context__.SourceCodeLine = 533;
            MakeString ( ENCODED , "{0}{1}", ENCODE_SNMPOID (  __context__ , OID) , ENCODE_SNMPNULL (  __context__  ) ) ; 
            __context__.SourceCodeLine = 535;
            MakeString ( RESPONSE , "{0}{1}{2}", Functions.Chr (  (int) ( 48 ) ) , ENCODE_SNMPLENGTH (  __context__ , (ushort)( Functions.Length( ENCODED ) )) , ENCODED ) ; 
            __context__.SourceCodeLine = 537;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPPDU (  SplusExecutionContext __context__, ushort MSGTYPE , ushort MSGID , CrestronString VARBIND ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 390, this );
            
            CrestronString ENCODED;
            ENCODED  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 384, this );
            
            
            __context__.SourceCodeLine = 545;
            MakeString ( ENCODED , "{0}{1}{2}", Functions.Chr (  (int) ( 48 ) ) , ENCODE_SNMPLENGTH (  __context__ , (ushort)( Functions.Length( VARBIND ) )) , VARBIND ) ; 
            __context__.SourceCodeLine = 547;
            MakeString ( ENCODED , "{0}{1}{2}{3}", ENCODE_SNMPINTEGER (  __context__ , (int)( MSGID )) , ENCODE_SNMPINTEGER (  __context__ , (int)( 0 )) , ENCODE_SNMPINTEGER (  __context__ , (int)( 0 )) , ENCODED ) ; 
            __context__.SourceCodeLine = 549;
            MakeString ( RESPONSE , "{0}{1}{2}", Functions.Chr (  (int) ( MSGTYPE ) ) , ENCODE_SNMPLENGTH (  __context__ , (ushort)( Functions.Length( ENCODED ) )) , ENCODED ) ; 
            __context__.SourceCodeLine = 551;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPMSG (  SplusExecutionContext __context__, ushort MSGTYPE , ushort MSGID , CrestronString VARBIND ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 520, this );
            
            CrestronString ENCODED;
            ENCODED  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
            
            
            __context__.SourceCodeLine = 559;
            MakeString ( ENCODED , "{0}{1}{2}", ENCODE_SNMPINTEGER (  __context__ , (int)( PROTOCOL  .Value )) , ENCODE_SNMPOCTETSTRING (  __context__ , COMMUNITY ) , ENCODE_SNMPPDU (  __context__ , (ushort)( MSGTYPE ), (ushort)( MSGID ), VARBIND) ) ; 
            __context__.SourceCodeLine = 561;
            MakeString ( RESPONSE , "{0}{1}{2}", Functions.Chr (  (int) ( 48 ) ) , ENCODE_SNMPLENGTH (  __context__ , (ushort)( Functions.Length( ENCODED ) )) , ENCODED ) ; 
            __context__.SourceCodeLine = 563;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPGET (  SplusExecutionContext __context__, ushort MSGID , CrestronString OID ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 520, this );
            
            
            __context__.SourceCodeLine = 573;
            RESPONSE  .UpdateValue ( ENCODE_SNMPMSG (  __context__ , (ushort)( 160 ), (ushort)( MSGID ), ENCODE_SNMPVARBIND_NULL( __context__ , OID ))  ) ; 
            __context__.SourceCodeLine = 575;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPGETNEXT (  SplusExecutionContext __context__, ushort MSGID , CrestronString OID ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 520, this );
            
            
            __context__.SourceCodeLine = 582;
            RESPONSE  .UpdateValue ( ENCODE_SNMPMSG (  __context__ , (ushort)( 161 ), (ushort)( MSGID ), ENCODE_SNMPVARBIND_NULL( __context__ , OID ))  ) ; 
            __context__.SourceCodeLine = 584;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPSET_STRING (  SplusExecutionContext __context__, ushort MSGID , CrestronString OID , CrestronString VALUE ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 520, this );
            
            
            __context__.SourceCodeLine = 591;
            RESPONSE  .UpdateValue ( ENCODE_SNMPMSG (  __context__ , (ushort)( 163 ), (ushort)( MSGID ), ENCODE_SNMPVARBIND_STRING( __context__ , OID , VALUE ))  ) ; 
            __context__.SourceCodeLine = 593;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString ENCODE_SNMPSET_INTEGER (  SplusExecutionContext __context__, ushort MSGID , CrestronString OID , int VALUE ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 520, this );
            
            
            __context__.SourceCodeLine = 600;
            RESPONSE  .UpdateValue ( ENCODE_SNMPMSG (  __context__ , (ushort)( 163 ), (ushort)( MSGID ), ENCODE_SNMPVARBIND_INTEGER( __context__ , OID , (int)( VALUE ) ))  ) ; 
            __context__.SourceCodeLine = 602;
            return ( RESPONSE ) ; 
            
            }
            
        private int DECODE_SNMPINTEGER (  SplusExecutionContext __context__, _SNMPMSGITEM ITEM ) 
            { 
            int RESPONSE = 0;
            
            uint TEMP = 0;
            
            ushort I = 0;
            
            
            __context__.SourceCodeLine = 614;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 2))  ) ) 
                { 
                __context__.SourceCodeLine = 616;
                TEMP = (uint) ( 0 ) ; 
                __context__.SourceCodeLine = 618;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)Functions.Length( ITEM.DATA ); 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 620;
                    TEMP = (uint) ( (TEMP + Byte( ITEM.DATA , (int)( I ) )) ) ; 
                    __context__.SourceCodeLine = 622;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( I < Functions.Length( ITEM.DATA ) ))  ) ) 
                        {
                        __context__.SourceCodeLine = 623;
                        TEMP = (uint) ( (TEMP << 8) ) ; 
                        }
                    
                    __context__.SourceCodeLine = 618;
                    } 
                
                __context__.SourceCodeLine = 626;
                RESPONSE = (int) ( TEMP ) ; 
                } 
            
            __context__.SourceCodeLine = 629;
            return (int)( RESPONSE) ; 
            
            }
            
        private uint DECODE_SNMPGAUGE (  SplusExecutionContext __context__, _SNMPMSGITEM ITEM ) 
            { 
            uint RESPONSE = 0;
            
            ushort I = 0;
            
            
            __context__.SourceCodeLine = 637;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 66))  ) ) 
                { 
                __context__.SourceCodeLine = 639;
                RESPONSE = (uint) ( 0 ) ; 
                __context__.SourceCodeLine = 641;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)Functions.Length( ITEM.DATA ); 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 643;
                    RESPONSE = (uint) ( (RESPONSE + Byte( ITEM.DATA , (int)( I ) )) ) ; 
                    __context__.SourceCodeLine = 645;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( I < Functions.Length( ITEM.DATA ) ))  ) ) 
                        {
                        __context__.SourceCodeLine = 646;
                        RESPONSE = (uint) ( (RESPONSE << 8) ) ; 
                        }
                    
                    __context__.SourceCodeLine = 641;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 650;
            return (uint)( RESPONSE) ; 
            
            }
            
        private uint DECODE_SNMPCOUNTER (  SplusExecutionContext __context__, _SNMPMSGITEM ITEM ) 
            { 
            uint RESPONSE = 0;
            
            ushort I = 0;
            
            
            __context__.SourceCodeLine = 658;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 65))  ) ) 
                { 
                __context__.SourceCodeLine = 660;
                RESPONSE = (uint) ( 0 ) ; 
                __context__.SourceCodeLine = 662;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)Functions.Length( ITEM.DATA ); 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 664;
                    RESPONSE = (uint) ( (RESPONSE + Byte( ITEM.DATA , (int)( I ) )) ) ; 
                    __context__.SourceCodeLine = 666;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( I < Functions.Length( ITEM.DATA ) ))  ) ) 
                        {
                        __context__.SourceCodeLine = 667;
                        RESPONSE = (uint) ( (RESPONSE << 8) ) ; 
                        }
                    
                    __context__.SourceCodeLine = 662;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 671;
            return (uint)( RESPONSE) ; 
            
            }
            
        private uint DECODE_SNMPTIMERTICKS (  SplusExecutionContext __context__, _SNMPMSGITEM ITEM ) 
            { 
            uint RESPONSE = 0;
            
            ushort I = 0;
            
            
            __context__.SourceCodeLine = 679;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 67))  ) ) 
                { 
                __context__.SourceCodeLine = 681;
                RESPONSE = (uint) ( 0 ) ; 
                __context__.SourceCodeLine = 683;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)Functions.Length( ITEM.DATA ); 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 685;
                    RESPONSE = (uint) ( (RESPONSE + Byte( ITEM.DATA , (int)( I ) )) ) ; 
                    __context__.SourceCodeLine = 687;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( I < Functions.Length( ITEM.DATA ) ))  ) ) 
                        {
                        __context__.SourceCodeLine = 688;
                        RESPONSE = (uint) ( (RESPONSE << 8) ) ; 
                        }
                    
                    __context__.SourceCodeLine = 683;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 692;
            return (uint)( RESPONSE) ; 
            
            }
            
        private CrestronString DECODE_SNMPOCTETSTRING (  SplusExecutionContext __context__, _SNMPMSGITEM ITEM ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 128, this );
            
            CrestronString TEMP;
            TEMP  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
            
            
            __context__.SourceCodeLine = 700;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 4))  ) ) 
                { 
                __context__.SourceCodeLine = 702;
                TEMP  .UpdateValue ( ITEM . DATA  ) ; 
                __context__.SourceCodeLine = 704;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( TEMP ) > 128 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 705;
                    MakeString ( RESPONSE , "{0}", Functions.Left ( TEMP ,  (int) ( 128 ) ) ) ; 
                    }
                
                else 
                    {
                    __context__.SourceCodeLine = 707;
                    MakeString ( RESPONSE , "{0}", TEMP ) ; 
                    }
                
                } 
            
            __context__.SourceCodeLine = 710;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString DECODE_SNMPIPADDRESS (  SplusExecutionContext __context__, _SNMPMSGITEM ITEM ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 128, this );
            
            ushort I = 0;
            
            
            __context__.SourceCodeLine = 718;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 64))  ) ) 
                { 
                __context__.SourceCodeLine = 720;
                RESPONSE  .UpdateValue ( ""  ) ; 
                __context__.SourceCodeLine = 722;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)Functions.Length( ITEM.DATA ); 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 724;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( I < Functions.Length( ITEM.DATA ) ))  ) ) 
                        {
                        __context__.SourceCodeLine = 725;
                        MakeString ( RESPONSE , "{0}{1}.", RESPONSE , Functions.ItoA (  (int) ( Byte( ITEM.DATA , (int)( I ) ) ) ) ) ; 
                        }
                    
                    else 
                        {
                        __context__.SourceCodeLine = 727;
                        MakeString ( RESPONSE , "{0}{1}", RESPONSE , Functions.ItoA (  (int) ( Byte( ITEM.DATA , (int)( I ) ) ) ) ) ; 
                        }
                    
                    __context__.SourceCodeLine = 722;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 731;
            return ( RESPONSE ) ; 
            
            }
            
        private CrestronString DECODE_SNMPOID (  SplusExecutionContext __context__, _SNMPMSGITEM ITEM ) 
            { 
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 128, this );
            
            ushort VALUE = 0;
            
            ushort I = 0;
            
            
            __context__.SourceCodeLine = 740;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 6))  ) ) 
                { 
                __context__.SourceCodeLine = 742;
                RESPONSE  .UpdateValue ( ""  ) ; 
                __context__.SourceCodeLine = 744;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)Functions.Length( ITEM.DATA ); 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 746;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (I == 1) ) && Functions.TestForTrue ( Functions.BoolToInt (Byte( ITEM.DATA , (int)( I ) ) == 43) )) ))  ) ) 
                        {
                        __context__.SourceCodeLine = 747;
                        RESPONSE  .UpdateValue ( "1.3"  ) ; 
                        }
                    
                    else 
                        {
                        __context__.SourceCodeLine = 748;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (I == 1) ) && Functions.TestForTrue ( Functions.BoolToInt (Byte( ITEM.DATA , (int)( I ) ) == 40) )) ))  ) ) 
                            {
                            __context__.SourceCodeLine = 749;
                            RESPONSE  .UpdateValue ( "1"  ) ; 
                            }
                        
                        else 
                            { 
                            __context__.SourceCodeLine = 752;
                            VALUE = (ushort) ( Byte( ITEM.DATA , (int)( I ) ) ) ; 
                            __context__.SourceCodeLine = 755;
                            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ((VALUE & 128) == 128))  ) ) 
                                { 
                                __context__.SourceCodeLine = 758;
                                VALUE = (ushort) ( (VALUE ^ 128) ) ; 
                                __context__.SourceCodeLine = 760;
                                I = (ushort) ( (I + 1) ) ; 
                                __context__.SourceCodeLine = 761;
                                VALUE = (ushort) ( ((VALUE * 128) + Byte( ITEM.DATA , (int)( I ) )) ) ; 
                                } 
                            
                            __context__.SourceCodeLine = 764;
                            MakeString ( RESPONSE , "{0}{1}", RESPONSE , Functions.ItoA (  (int) ( VALUE ) ) ) ; 
                            } 
                        
                        }
                    
                    __context__.SourceCodeLine = 767;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( I < Functions.Length( ITEM.DATA ) ))  ) ) 
                        {
                        __context__.SourceCodeLine = 768;
                        MakeString ( RESPONSE , "{0}.", RESPONSE ) ; 
                        }
                    
                    __context__.SourceCodeLine = 744;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 772;
            return ( RESPONSE ) ; 
            
            }
            
        private void DECODE_SNMPVARBIND_VALUE (  SplusExecutionContext __context__, _SNMPMSGITEM ITEM , _SNMPMSG_VARBIND VARBIND ) 
            { 
            
            __context__.SourceCodeLine = 778;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 5))  ) ) 
                { 
                __context__.SourceCodeLine = 780;
                VARBIND . TYPE = (ushort) ( ITEM.TAG ) ; 
                __context__.SourceCodeLine = 781;
                VARBIND . SVALUE  .UpdateValue ( ""  ) ; 
                __context__.SourceCodeLine = 782;
                VARBIND . NVALUE = (uint) ( 0 ) ; 
                __context__.SourceCodeLine = 783;
                VARBIND . SNVALUE = (int) ( Functions.ToLongInteger( -( 1 ) ) ) ; 
                } 
            
            else 
                {
                __context__.SourceCodeLine = 786;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 2))  ) ) 
                    { 
                    __context__.SourceCodeLine = 788;
                    VARBIND . TYPE = (ushort) ( ITEM.TAG ) ; 
                    __context__.SourceCodeLine = 789;
                    VARBIND . SVALUE  .UpdateValue ( ""  ) ; 
                    __context__.SourceCodeLine = 790;
                    VARBIND . NVALUE = (uint) ( 0 ) ; 
                    __context__.SourceCodeLine = 791;
                    VARBIND . SNVALUE = (int) ( DECODE_SNMPINTEGER( __context__ , ITEM ) ) ; 
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 794;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 66))  ) ) 
                        { 
                        __context__.SourceCodeLine = 796;
                        VARBIND . TYPE = (ushort) ( ITEM.TAG ) ; 
                        __context__.SourceCodeLine = 797;
                        VARBIND . SVALUE  .UpdateValue ( ""  ) ; 
                        __context__.SourceCodeLine = 798;
                        VARBIND . NVALUE = (uint) ( DECODE_SNMPGAUGE( __context__ , ITEM ) ) ; 
                        __context__.SourceCodeLine = 799;
                        VARBIND . SNVALUE = (int) ( Functions.ToLongInteger( -( 1 ) ) ) ; 
                        } 
                    
                    else 
                        {
                        __context__.SourceCodeLine = 802;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 65))  ) ) 
                            { 
                            __context__.SourceCodeLine = 804;
                            VARBIND . TYPE = (ushort) ( ITEM.TAG ) ; 
                            __context__.SourceCodeLine = 805;
                            VARBIND . SVALUE  .UpdateValue ( ""  ) ; 
                            __context__.SourceCodeLine = 806;
                            VARBIND . NVALUE = (uint) ( DECODE_SNMPCOUNTER( __context__ , ITEM ) ) ; 
                            __context__.SourceCodeLine = 807;
                            VARBIND . SNVALUE = (int) ( Functions.ToLongInteger( -( 1 ) ) ) ; 
                            } 
                        
                        else 
                            {
                            __context__.SourceCodeLine = 810;
                            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 67))  ) ) 
                                { 
                                __context__.SourceCodeLine = 812;
                                VARBIND . TYPE = (ushort) ( ITEM.TAG ) ; 
                                __context__.SourceCodeLine = 813;
                                VARBIND . SVALUE  .UpdateValue ( ""  ) ; 
                                __context__.SourceCodeLine = 814;
                                VARBIND . NVALUE = (uint) ( DECODE_SNMPTIMERTICKS( __context__ , ITEM ) ) ; 
                                __context__.SourceCodeLine = 815;
                                VARBIND . SNVALUE = (int) ( Functions.ToLongInteger( -( 1 ) ) ) ; 
                                } 
                            
                            else 
                                {
                                __context__.SourceCodeLine = 818;
                                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 4))  ) ) 
                                    { 
                                    __context__.SourceCodeLine = 820;
                                    VARBIND . TYPE = (ushort) ( ITEM.TAG ) ; 
                                    __context__.SourceCodeLine = 821;
                                    VARBIND . SVALUE  .UpdateValue ( DECODE_SNMPOCTETSTRING (  __context__ , ITEM)  ) ; 
                                    __context__.SourceCodeLine = 822;
                                    VARBIND . NVALUE = (uint) ( 0 ) ; 
                                    __context__.SourceCodeLine = 823;
                                    VARBIND . SNVALUE = (int) ( Functions.ToLongInteger( -( 1 ) ) ) ; 
                                    } 
                                
                                else 
                                    {
                                    __context__.SourceCodeLine = 826;
                                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ITEM.TAG == 64))  ) ) 
                                        { 
                                        __context__.SourceCodeLine = 828;
                                        VARBIND . TYPE = (ushort) ( ITEM.TAG ) ; 
                                        __context__.SourceCodeLine = 829;
                                        VARBIND . SVALUE  .UpdateValue ( DECODE_SNMPIPADDRESS (  __context__ , ITEM)  ) ; 
                                        __context__.SourceCodeLine = 830;
                                        VARBIND . NVALUE = (uint) ( 0 ) ; 
                                        __context__.SourceCodeLine = 831;
                                        VARBIND . SNVALUE = (int) ( Functions.ToLongInteger( -( 1 ) ) ) ; 
                                        } 
                                    
                                    }
                                
                                }
                            
                            }
                        
                        }
                    
                    }
                
                }
            
            
            }
            
        private ushort DECODE_SNMPLENGTH (  SplusExecutionContext __context__, CrestronString BUFFER ) 
            { 
            ushort RESPONSE = 0;
            
            ushort QTY = 0;
            
            ushort I = 0;
            
            
            __context__.SourceCodeLine = 841;
            RESPONSE = (ushort) ( Byte( Functions.Remove( 1 , BUFFER ) , (int)( 1 ) ) ) ; 
            __context__.SourceCodeLine = 843;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ((RESPONSE & 128) == 128))  ) ) 
                { 
                __context__.SourceCodeLine = 845;
                QTY = (ushort) ( (RESPONSE ^ 128) ) ; 
                __context__.SourceCodeLine = 847;
                RESPONSE = (ushort) ( 0 ) ; 
                __context__.SourceCodeLine = 849;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)QTY; 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 851;
                    RESPONSE = (ushort) ( (RESPONSE + Byte( Functions.Remove( 1 , BUFFER ) , (int)( 1 ) )) ) ; 
                    __context__.SourceCodeLine = 853;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( I < QTY ))  ) ) 
                        {
                        __context__.SourceCodeLine = 854;
                        RESPONSE = (ushort) ( (RESPONSE << 8) ) ; 
                        }
                    
                    __context__.SourceCodeLine = 849;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 858;
            return (ushort)( RESPONSE) ; 
            
            }
            
        private void DECODE_SNMPMSGITEM (  SplusExecutionContext __context__, CrestronString BUFFER , _SNMPMSGITEM ITEM ) 
            { 
            ushort LENGTH = 0;
            
            
            __context__.SourceCodeLine = 865;
            ITEM . TAG = (ushort) ( Byte( Functions.Remove( 1 , BUFFER ) , (int)( 1 ) ) ) ; 
            __context__.SourceCodeLine = 866;
            ITEM . LENGTH = (ushort) ( DECODE_SNMPLENGTH( __context__ , BUFFER ) ) ; 
            __context__.SourceCodeLine = 867;
            LENGTH = (ushort) ( ITEM.LENGTH ) ; 
            __context__.SourceCodeLine = 869;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( BUFFER ) >= LENGTH ))  ) ) 
                {
                __context__.SourceCodeLine = 870;
                ITEM . DATA  .UpdateValue ( Functions.Remove ( LENGTH, BUFFER )  ) ; 
                }
            
            else 
                {
                __context__.SourceCodeLine = 872;
                ITEM . DATA  .UpdateValue ( ""  ) ; 
                }
            
            
            }
            
        private ushort TEST_SNMPMSGITEM (  SplusExecutionContext __context__, CrestronString BUFFER ) 
            { 
            ushort TAG = 0;
            
            ushort LENGTH = 0;
            
            CrestronString TEMP;
            TEMP  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
            
            
            __context__.SourceCodeLine = 881;
            TEMP  .UpdateValue ( BUFFER  ) ; 
            __context__.SourceCodeLine = 883;
            TAG = (ushort) ( Byte( Functions.Remove( 1 , TEMP ) , (int)( 1 ) ) ) ; 
            __context__.SourceCodeLine = 885;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (TAG == 48))  ) ) 
                { 
                __context__.SourceCodeLine = 887;
                LENGTH = (ushort) ( DECODE_SNMPLENGTH( __context__ , TEMP ) ) ; 
                __context__.SourceCodeLine = 889;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( TEMP ) >= LENGTH ))  ) ) 
                    {
                    __context__.SourceCodeLine = 890;
                    return (ushort)( 1) ; 
                    }
                
                } 
            
            __context__.SourceCodeLine = 893;
            return (ushort)( 0) ; 
            
            }
            
        private ushort DECODE_SNMPMSG (  SplusExecutionContext __context__, CrestronString BUFFER ) 
            { 
            ushort RESPONSE = 0;
            
            CrestronString PARSE;
            PARSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
            
            ushort OBJECTITEM = 0;
            
            ushort ITEM = 0;
            
            
            __context__.SourceCodeLine = 908;
            MakeString ( PARSE , "{0}", BUFFER ) ; 
            __context__.SourceCodeLine = 910;
            Functions.ClearBuffer ( BUFFER ) ; 
            __context__.SourceCodeLine = 912;
            OBJECTITEM = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 913;
            ITEM = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 915;
            RESPONSE = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 917;
            while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( PARSE ) > 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 919;
                DECODE_SNMPMSGITEM (  __context__ , PARSE, PARSEDSNMPMSGITEM) ; 
                __context__.SourceCodeLine = 922;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Length( PARSEDSNMPMSGITEM.DATA ) == 0))  ) ) 
                    {
                    __context__.SourceCodeLine = 923;
                    break ; 
                    }
                
                __context__.SourceCodeLine = 925;
                
                    {
                    int __SPLS_TMPVAR__SWTCH_1__ = ((int)OBJECTITEM);
                    
                        { 
                        if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 0) ) ) ) 
                            { 
                            __context__.SourceCodeLine = 930;
                            
                                {
                                int __SPLS_TMPVAR__SWTCH_2__ = ((int)ITEM);
                                
                                    { 
                                    if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_2__ == ( 0) ) ) ) 
                                        { 
                                        __context__.SourceCodeLine = 935;
                                        PARSE  .UpdateValue ( PARSEDSNMPMSGITEM . DATA  ) ; 
                                        __context__.SourceCodeLine = 936;
                                        ITEM = (ushort) ( (ITEM + 1) ) ; 
                                        } 
                                    
                                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_2__ == ( 1) ) ) ) 
                                        { 
                                        __context__.SourceCodeLine = 941;
                                        PARSEDSNMPMSG . VERSION = (uint) ( DECODE_SNMPINTEGER( __context__ , PARSEDSNMPMSGITEM ) ) ; 
                                        __context__.SourceCodeLine = 942;
                                        ITEM = (ushort) ( (ITEM + 1) ) ; 
                                        } 
                                    
                                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_2__ == ( 2) ) ) ) 
                                        { 
                                        __context__.SourceCodeLine = 947;
                                        PARSEDSNMPMSG . COMMUNITY  .UpdateValue ( DECODE_SNMPOCTETSTRING (  __context__ , PARSEDSNMPMSGITEM)  ) ; 
                                        __context__.SourceCodeLine = 948;
                                        ITEM = (ushort) ( (ITEM + 1) ) ; 
                                        } 
                                    
                                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_2__ == ( 3) ) ) ) 
                                        { 
                                        __context__.SourceCodeLine = 953;
                                        PARSE  .UpdateValue ( PARSEDSNMPMSGITEM . DATA  ) ; 
                                        __context__.SourceCodeLine = 954;
                                        ITEM = (ushort) ( 0 ) ; 
                                        __context__.SourceCodeLine = 955;
                                        OBJECTITEM = (ushort) ( (OBJECTITEM + 1) ) ; 
                                        } 
                                    
                                    } 
                                    
                                }
                                
                            
                            } 
                        
                        else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 1) ) ) ) 
                            { 
                            __context__.SourceCodeLine = 962;
                            
                                {
                                int __SPLS_TMPVAR__SWTCH_3__ = ((int)ITEM);
                                
                                    { 
                                    if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_3__ == ( 0) ) ) ) 
                                        { 
                                        __context__.SourceCodeLine = 967;
                                        PARSEDSNMPMSG_PDU . MSGID = (uint) ( DECODE_SNMPINTEGER( __context__ , PARSEDSNMPMSGITEM ) ) ; 
                                        __context__.SourceCodeLine = 968;
                                        ITEM = (ushort) ( (ITEM + 1) ) ; 
                                        } 
                                    
                                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_3__ == ( 1) ) ) ) 
                                        { 
                                        __context__.SourceCodeLine = 973;
                                        PARSEDSNMPMSG_PDU . ERROR = (uint) ( DECODE_SNMPINTEGER( __context__ , PARSEDSNMPMSGITEM ) ) ; 
                                        __context__.SourceCodeLine = 974;
                                        ITEM = (ushort) ( (ITEM + 1) ) ; 
                                        } 
                                    
                                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_3__ == ( 2) ) ) ) 
                                        { 
                                        __context__.SourceCodeLine = 979;
                                        PARSEDSNMPMSG_PDU . ERRORINDEX = (uint) ( DECODE_SNMPINTEGER( __context__ , PARSEDSNMPMSGITEM ) ) ; 
                                        __context__.SourceCodeLine = 980;
                                        ITEM = (ushort) ( (ITEM + 1) ) ; 
                                        } 
                                    
                                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_3__ == ( 3) ) ) ) 
                                        { 
                                        __context__.SourceCodeLine = 985;
                                        PARSE  .UpdateValue ( PARSEDSNMPMSGITEM . DATA  ) ; 
                                        __context__.SourceCodeLine = 986;
                                        ITEM = (ushort) ( 0 ) ; 
                                        __context__.SourceCodeLine = 987;
                                        OBJECTITEM = (ushort) ( (OBJECTITEM + 1) ) ; 
                                        } 
                                    
                                    } 
                                    
                                }
                                
                            
                            } 
                        
                        else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 2) ) ) ) 
                            { 
                            __context__.SourceCodeLine = 994;
                            
                                {
                                int __SPLS_TMPVAR__SWTCH_4__ = ((int)ITEM);
                                
                                    { 
                                    if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_4__ == ( 0) ) ) ) 
                                        { 
                                        __context__.SourceCodeLine = 999;
                                        PARSE  .UpdateValue ( PARSEDSNMPMSGITEM . DATA  ) ; 
                                        __context__.SourceCodeLine = 1000;
                                        ITEM = (ushort) ( (ITEM + 1) ) ; 
                                        } 
                                    
                                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_4__ == ( 1) ) ) ) 
                                        { 
                                        __context__.SourceCodeLine = 1005;
                                        PARSEDSNMPMSG_VARBIND . OID  .UpdateValue ( DECODE_SNMPOID (  __context__ , PARSEDSNMPMSGITEM)  ) ; 
                                        __context__.SourceCodeLine = 1006;
                                        ITEM = (ushort) ( (ITEM + 1) ) ; 
                                        } 
                                    
                                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_4__ == ( 2) ) ) ) 
                                        { 
                                        __context__.SourceCodeLine = 1011;
                                        DECODE_SNMPVARBIND_VALUE (  __context__ , PARSEDSNMPMSGITEM, PARSEDSNMPMSG_VARBIND) ; 
                                        __context__.SourceCodeLine = 1012;
                                        ITEM = (ushort) ( 0 ) ; 
                                        __context__.SourceCodeLine = 1013;
                                        OBJECTITEM = (ushort) ( 0 ) ; 
                                        __context__.SourceCodeLine = 1014;
                                        PARSE  .UpdateValue ( ""  ) ; 
                                        __context__.SourceCodeLine = 1015;
                                        RESPONSE = (ushort) ( 1 ) ; 
                                        } 
                                    
                                    } 
                                    
                                }
                                
                            
                            } 
                        
                        } 
                        
                    }
                    
                
                __context__.SourceCodeLine = 917;
                } 
            
            __context__.SourceCodeLine = 1022;
            return (ushort)( RESPONSE) ; 
            
            }
            
        private ushort ISQUEUEEMPTY (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 1030;
            return (ushort)( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (QUEUECOMM.NHASITEMS == 0) ) && Functions.TestForTrue ( Functions.BoolToInt (QUEUECOMM.NBUSY == 0) )) )) ; 
            
            }
            
        private CrestronString DEQUEUE (  SplusExecutionContext __context__ ) 
            { 
            CrestronString SRESPONSE;
            SRESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
            
            
            __context__.SourceCodeLine = 1037;
            SRESPONSE  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 1039;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (QUEUECOMM.NHASITEMS == 1) ) && Functions.TestForTrue ( Functions.BoolToInt (QUEUECOMM.NBUSY == 0) )) ))  ) ) 
                { 
                __context__.SourceCodeLine = 1041;
                QUEUECOMM . NBUSY = (ushort) ( 1 ) ; 
                __context__.SourceCodeLine = 1044;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (QUEUECOMM.NCOMMANDHEAD != QUEUECOMM.NCOMMANDTAIL))  ) ) 
                    { 
                    __context__.SourceCodeLine = 1046;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (QUEUECOMM.NCOMMANDTAIL == Functions.GetNumArrayRows( SCOMMANDQUEUE )))  ) ) 
                        {
                        __context__.SourceCodeLine = 1047;
                        QUEUECOMM . NCOMMANDTAIL = (ushort) ( 1 ) ; 
                        }
                    
                    else 
                        {
                        __context__.SourceCodeLine = 1049;
                        QUEUECOMM . NCOMMANDTAIL = (ushort) ( (QUEUECOMM.NCOMMANDTAIL + 1) ) ; 
                        }
                    
                    __context__.SourceCodeLine = 1051;
                    QUEUECOMM . SLASTMESSAGE  .UpdateValue ( SCOMMANDQUEUE [ QUEUECOMM.NCOMMANDTAIL ]  ) ; 
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 1054;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (QUEUECOMM.NSTATUSHEAD != QUEUECOMM.NSTATUSTAIL))  ) ) 
                        { 
                        __context__.SourceCodeLine = 1056;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (QUEUECOMM.NSTATUSTAIL == Functions.GetNumArrayRows( SSTATUSQUEUE )))  ) ) 
                            {
                            __context__.SourceCodeLine = 1057;
                            QUEUECOMM . NSTATUSTAIL = (ushort) ( 1 ) ; 
                            }
                        
                        else 
                            {
                            __context__.SourceCodeLine = 1059;
                            QUEUECOMM . NSTATUSTAIL = (ushort) ( (QUEUECOMM.NSTATUSTAIL + 1) ) ; 
                            }
                        
                        __context__.SourceCodeLine = 1061;
                        QUEUECOMM . SLASTMESSAGE  .UpdateValue ( SSTATUSQUEUE [ QUEUECOMM.NSTATUSTAIL ]  ) ; 
                        } 
                    
                    }
                
                __context__.SourceCodeLine = 1064;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (QUEUECOMM.NCOMMANDHEAD == QUEUECOMM.NCOMMANDTAIL) ) && Functions.TestForTrue ( Functions.BoolToInt (QUEUECOMM.NSTATUSHEAD == QUEUECOMM.NSTATUSTAIL) )) ))  ) ) 
                    {
                    __context__.SourceCodeLine = 1065;
                    QUEUECOMM . NHASITEMS = (ushort) ( 0 ) ; 
                    }
                
                __context__.SourceCodeLine = 1067;
                SRESPONSE  .UpdateValue ( QUEUECOMM . SLASTMESSAGE  ) ; 
                } 
            
            __context__.SourceCodeLine = 1070;
            return ( SRESPONSE ) ; 
            
            }
            
        private void SENDNEXTQUEUEITEM (  SplusExecutionContext __context__ ) 
            { 
            CrestronString SOUTGOING;
            SOUTGOING  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
            
            
            __context__.SourceCodeLine = 1077;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (QUEUECOMM.NRESENDLAST == 1))  ) ) 
                { 
                __context__.SourceCodeLine = 1079;
                QUEUECOMM . NRESENDLAST = (ushort) ( 0 ) ; 
                __context__.SourceCodeLine = 1080;
                SOUTGOING  .UpdateValue ( QUEUECOMM . SLASTMESSAGE  ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 1084;
                SOUTGOING  .UpdateValue ( DEQUEUE (  __context__  )  ) ; 
                } 
            
            __context__.SourceCodeLine = 1087;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( SOUTGOING ) > 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 1089;
                SOCKETSENDPACKET (  __context__ , SOUTGOING) ; 
                __context__.SourceCodeLine = 1091;
                CreateWait ( "QUEUEFALSERESPONSE" , 500 , QUEUEFALSERESPONSE_Callback ) ;
                } 
            
            
            }
            
        public void QUEUEFALSERESPONSE_CallbackFn( object stateInfo )
        {
        
            try
            {
                Wait __LocalWait__ = (Wait)stateInfo;
                SplusExecutionContext __context__ = SplusThreadStartCode(__LocalWait__);
                __LocalWait__.RemoveFromList();
                
            
            __context__.SourceCodeLine = 1093;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (QUEUECOMM.NBUSY == 1))  ) ) 
                { 
                __context__.SourceCodeLine = 1095;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( QUEUECOMM.NSTRIKECOUNT < 3 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 1097;
                    QUEUECOMM . NSTRIKECOUNT = (ushort) ( (QUEUECOMM.NSTRIKECOUNT + 1) ) ; 
                    __context__.SourceCodeLine = 1099;
                    /* Trace( "!!!FAILED Response") */ ; 
                    __context__.SourceCodeLine = 1101;
                    QUEUECOMM . NRESENDLAST = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 1103;
                    SENDNEXTQUEUEITEM (  __context__  ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 1107;
                    IS_COMMUNICATING  .Value = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 1108;
                    RESET (  __context__  ) ; 
                    } 
                
                } 
            
            
        
        
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler(); }
            
        }
        
    private void ENQUEUE (  SplusExecutionContext __context__, CrestronString SCMD , ushort BPRIORITY ) 
        { 
        ushort NQUEUEWASEMPTY = 0;
        
        
        __context__.SourceCodeLine = 1119;
        NQUEUEWASEMPTY = (ushort) ( ISQUEUEEMPTY( __context__ ) ) ; 
        __context__.SourceCodeLine = 1121;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (BPRIORITY == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 1123;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (QUEUECOMM.NCOMMANDHEAD == Functions.GetNumArrayRows( SCOMMANDQUEUE )))  ) ) 
                { 
                __context__.SourceCodeLine = 1125;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (QUEUECOMM.NCOMMANDTAIL != 1))  ) ) 
                    { 
                    __context__.SourceCodeLine = 1127;
                    QUEUECOMM . NCOMMANDHEAD = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 1128;
                    SCOMMANDQUEUE [ QUEUECOMM.NCOMMANDHEAD ]  .UpdateValue ( SCMD  ) ; 
                    __context__.SourceCodeLine = 1129;
                    QUEUECOMM . NHASITEMS = (ushort) ( 1 ) ; 
                    } 
                
                } 
            
            else 
                {
                __context__.SourceCodeLine = 1132;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (QUEUECOMM.NCOMMANDTAIL != (QUEUECOMM.NCOMMANDHEAD + 1)))  ) ) 
                    { 
                    __context__.SourceCodeLine = 1134;
                    QUEUECOMM . NCOMMANDHEAD = (ushort) ( (QUEUECOMM.NCOMMANDHEAD + 1) ) ; 
                    __context__.SourceCodeLine = 1135;
                    SCOMMANDQUEUE [ QUEUECOMM.NCOMMANDHEAD ]  .UpdateValue ( SCMD  ) ; 
                    __context__.SourceCodeLine = 1136;
                    QUEUECOMM . NHASITEMS = (ushort) ( 1 ) ; 
                    } 
                
                }
            
            } 
        
        else 
            { 
            __context__.SourceCodeLine = 1141;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (QUEUECOMM.NSTATUSHEAD == Functions.GetNumArrayRows( SSTATUSQUEUE )))  ) ) 
                { 
                __context__.SourceCodeLine = 1143;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (QUEUECOMM.NSTATUSTAIL != 1))  ) ) 
                    { 
                    __context__.SourceCodeLine = 1145;
                    QUEUECOMM . NSTATUSHEAD = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 1146;
                    SSTATUSQUEUE [ QUEUECOMM.NSTATUSHEAD ]  .UpdateValue ( SCMD  ) ; 
                    __context__.SourceCodeLine = 1147;
                    QUEUECOMM . NHASITEMS = (ushort) ( 1 ) ; 
                    } 
                
                } 
            
            else 
                {
                __context__.SourceCodeLine = 1150;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (QUEUECOMM.NSTATUSTAIL != (QUEUECOMM.NSTATUSHEAD + 1)))  ) ) 
                    { 
                    __context__.SourceCodeLine = 1152;
                    QUEUECOMM . NSTATUSHEAD = (ushort) ( (QUEUECOMM.NSTATUSHEAD + 1) ) ; 
                    __context__.SourceCodeLine = 1153;
                    SSTATUSQUEUE [ QUEUECOMM.NSTATUSHEAD ]  .UpdateValue ( SCMD  ) ; 
                    __context__.SourceCodeLine = 1154;
                    QUEUECOMM . NHASITEMS = (ushort) ( 1 ) ; 
                    } 
                
                }
            
            } 
        
        __context__.SourceCodeLine = 1158;
        if ( Functions.TestForTrue  ( ( NQUEUEWASEMPTY)  ) ) 
            {
            __context__.SourceCodeLine = 1159;
            SENDNEXTQUEUEITEM (  __context__  ) ; 
            }
        
        
        }
        
    private void GOODRESPONSE (  SplusExecutionContext __context__ ) 
        { 
        
        __context__.SourceCodeLine = 1165;
        /* Trace( "GOOD Response!") */ ; 
        __context__.SourceCodeLine = 1167;
        QUEUECOMM . NBUSY = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1169;
        CancelWait ( "QUEUEFALSERESPONSE" ) ; 
        __context__.SourceCodeLine = 1171;
        QUEUECOMM . NSTRIKECOUNT = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1173;
        QUEUECOMM . NRESENDLAST = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1175;
        IS_COMMUNICATING  .Value = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 1177;
        SENDNEXTQUEUEITEM (  __context__  ) ; 
        
        }
        
    private void PACKMODULERESPONSE (  SplusExecutionContext __context__, _SNMPMSG_PDU PDU , _SNMPMSG_VARBIND VARBIND ) 
        { 
        CrestronString VALUE;
        VALUE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 128, this );
        
        CrestronString TYPE;
        TYPE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, this );
        
        
        __context__.SourceCodeLine = 1188;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( PDU.MSGID > 0 ) ) && Functions.TestForTrue ( Functions.BoolToInt ( PDU.MSGID <= 50 ) )) ))  ) ) 
            { 
            __context__.SourceCodeLine = 1190;
            
                {
                int __SPLS_TMPVAR__SWTCH_5__ = ((int)VARBIND.TYPE);
                
                    { 
                    if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_5__ == ( 2) ) ) ) 
                        { 
                        __context__.SourceCodeLine = 1194;
                        VALUE  .UpdateValue ( Functions.LtoA (  (int) ( VARBIND.SNVALUE ) )  ) ; 
                        __context__.SourceCodeLine = 1195;
                        TYPE  .UpdateValue ( "INTEGER"  ) ; 
                        } 
                    
                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_5__ == ( 4) ) ) ) 
                        { 
                        __context__.SourceCodeLine = 1199;
                        VALUE  .UpdateValue ( VARBIND . SVALUE  ) ; 
                        __context__.SourceCodeLine = 1200;
                        TYPE  .UpdateValue ( "STRING"  ) ; 
                        } 
                    
                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_5__ == ( 64) ) ) ) 
                        { 
                        __context__.SourceCodeLine = 1204;
                        VALUE  .UpdateValue ( VARBIND . SVALUE  ) ; 
                        __context__.SourceCodeLine = 1205;
                        TYPE  .UpdateValue ( "IP_ADDRESS"  ) ; 
                        } 
                    
                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_5__ == ( 65) ) ) ) 
                        { 
                        __context__.SourceCodeLine = 1209;
                        VALUE  .UpdateValue ( Functions.LtoA (  (int) ( VARBIND.NVALUE ) )  ) ; 
                        __context__.SourceCodeLine = 1210;
                        TYPE  .UpdateValue ( "COUNTER"  ) ; 
                        } 
                    
                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_5__ == ( 66) ) ) ) 
                        { 
                        __context__.SourceCodeLine = 1214;
                        VALUE  .UpdateValue ( Functions.LtoA (  (int) ( VARBIND.NVALUE ) )  ) ; 
                        __context__.SourceCodeLine = 1215;
                        TYPE  .UpdateValue ( "GAUGE"  ) ; 
                        } 
                    
                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_5__ == ( 67) ) ) ) 
                        { 
                        __context__.SourceCodeLine = 1219;
                        VALUE  .UpdateValue ( Functions.LtoA (  (int) ( VARBIND.NVALUE ) )  ) ; 
                        __context__.SourceCodeLine = 1220;
                        TYPE  .UpdateValue ( "TIMERTICKS"  ) ; 
                        } 
                    
                    } 
                    
                }
                
            
            __context__.SourceCodeLine = 1224;
            MakeString ( TO_MODULES [ PDU.MSGID] , "RESPONSE<{0}|{1}|{2}>", TYPE , VARBIND . OID , VALUE ) ; 
            } 
        
        
        }
        
    private void BUILDSNMPMESSAGESET (  SplusExecutionContext __context__, ushort ID , CrestronString TYPE , CrestronString OID , CrestronString VALUE ) 
        { 
        CrestronString OUT;
        OUT  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
        
        
        __context__.SourceCodeLine = 1232;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (TYPE == "INTEGER"))  ) ) 
            { 
            __context__.SourceCodeLine = 1234;
            OUT  .UpdateValue ( ENCODE_SNMPSET_INTEGER (  __context__ , (ushort)( ID ), OID, (int)( ATOL_SIGNED( __context__ , VALUE ) ))  ) ; 
            } 
        
        else 
            {
            __context__.SourceCodeLine = 1236;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (TYPE == "STRING"))  ) ) 
                { 
                __context__.SourceCodeLine = 1238;
                OUT  .UpdateValue ( ENCODE_SNMPSET_STRING (  __context__ , (ushort)( ID ), OID, VALUE)  ) ; 
                } 
            
            }
        
        __context__.SourceCodeLine = 1241;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( OUT ) > 0 ))  ) ) 
            {
            __context__.SourceCodeLine = 1242;
            ENQUEUE (  __context__ , OUT, (ushort)( 1 )) ; 
            }
        
        
        }
        
    private void SENDHEARTBEAT (  SplusExecutionContext __context__ ) 
        { 
        CrestronString OUTGOINGMSG;
        OUTGOINGMSG  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
        
        
        __context__.SourceCodeLine = 1252;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ISHEARTBEATING == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 1254;
            if ( Functions.TestForTrue  ( ( ISQUEUEEMPTY( __context__ ))  ) ) 
                { 
                __context__.SourceCodeLine = 1256;
                OUTGOINGMSG  .UpdateValue ( ENCODE_SNMPGETNEXT (  __context__ , (ushort)( 0 ), "1")  ) ; 
                __context__.SourceCodeLine = 1258;
                ENQUEUE (  __context__ , OUTGOINGMSG, (ushort)( 0 )) ; 
                } 
            
            __context__.SourceCodeLine = 1261;
            CreateWait ( "HEARTBEAT" , 3000 , HEARTBEAT_Callback ) ;
            } 
        
        
        }
        
    public void HEARTBEAT_CallbackFn( object stateInfo )
    {
    
        try
        {
            Wait __LocalWait__ = (Wait)stateInfo;
            SplusExecutionContext __context__ = SplusThreadStartCode(__LocalWait__);
            __LocalWait__.RemoveFromList();
            
            
            __context__.SourceCodeLine = 1263;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ISHEARTBEATING == 1))  ) ) 
                {
                __context__.SourceCodeLine = 1264;
                SENDHEARTBEAT (  __context__  ) ; 
                }
            
            
        
        
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler(); }
        
    }
    
private void STARTHEARTBEAT (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 1271;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ISHEARTBEATING == 0))  ) ) 
        { 
        __context__.SourceCodeLine = 1273;
        ISHEARTBEATING = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 1275;
        SENDHEARTBEAT (  __context__  ) ; 
        } 
    
    
    }
    
private void STOPHEARTBEAT (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 1281;
    ISHEARTBEATING = (ushort) ( 0 ) ; 
    __context__.SourceCodeLine = 1283;
    CancelWait ( "HEARTBEAT" ) ; 
    
    }
    
private void REGISTRATIONPROCESS (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 1291;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( REGMODULEID < Functions.GetNumArrayRows( TO_MODULES ) ))  ) ) 
        { 
        __context__.SourceCodeLine = 1293;
        REGMODULEID = (ushort) ( (REGMODULEID + 1) ) ; 
        __context__.SourceCodeLine = 1295;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (IsSignalDefined( TO_MODULES[ REGMODULEID ] ) == 1))  ) ) 
            {
            __context__.SourceCodeLine = 1296;
            MakeString ( TO_MODULES [ REGMODULEID] , "REGISTER<{0:d}>", (short)REGMODULEID) ; 
            }
        
        __context__.SourceCodeLine = 1298;
        CreateWait ( "REGISTRATIONWAIT" , 10 , REGISTRATIONWAIT_Callback ) ;
        } 
    
    else 
        { 
        __context__.SourceCodeLine = 1306;
        ISREGISTERING = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1308;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (CONNECT  .Value == 1))  ) ) 
            {
            __context__.SourceCodeLine = 1309;
            STARTHEARTBEAT (  __context__  ) ; 
            }
        
        } 
    
    
    }
    
public void REGISTRATIONWAIT_CallbackFn( object stateInfo )
{

    try
    {
        Wait __LocalWait__ = (Wait)stateInfo;
        SplusExecutionContext __context__ = SplusThreadStartCode(__LocalWait__);
        __LocalWait__.RemoveFromList();
        
            
            __context__.SourceCodeLine = 1300;
            if ( Functions.TestForTrue  ( ( ISREGISTERING)  ) ) 
                {
                __context__.SourceCodeLine = 1301;
                REGISTRATIONPROCESS (  __context__  ) ; 
                }
            
            
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    
}

private void STARTREGISTRATION (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 1315;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ISREGISTERING == 0))  ) ) 
        { 
        __context__.SourceCodeLine = 1317;
        ISREGISTERING = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 1318;
        REGMODULEID = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1319;
        REGISTRATIONPROCESS (  __context__  ) ; 
        } 
    
    
    }
    
private void STOPREGISTRATION (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 1325;
    CancelWait ( "REGISTRATIONWAIT" ) ; 
    __context__.SourceCodeLine = 1326;
    ISREGISTERING = (ushort) ( 0 ) ; 
    
    }
    
object CONNECT_OnPush_0 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        short STATUS = 0;
        
        
        __context__.SourceCodeLine = 1337;
        RESET (  __context__  ) ; 
        __context__.SourceCodeLine = 1338;
        CancelWait ( "QUEUEFALSERESPONSE" ) ; 
        __context__.SourceCodeLine = 1340;
        STATUS = (short) ( Functions.SocketUDP_Enable( SNMPAGENT , IPADDRESS  , (ushort)( 161 ) ) ) ; 
        __context__.SourceCodeLine = 1342;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (STATUS == 0))  ) ) 
            { 
            __context__.SourceCodeLine = 1344;
            ISCONNECTED = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 1347;
            CreateWait ( "STARTHEARTBEATPROCESS" , 500 , STARTHEARTBEATPROCESS_Callback ) ;
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

public void STARTHEARTBEATPROCESS_CallbackFn( object stateInfo )
{

    try
    {
        Wait __LocalWait__ = (Wait)stateInfo;
        SplusExecutionContext __context__ = SplusThreadStartCode(__LocalWait__);
        __LocalWait__.RemoveFromList();
        
            
            __context__.SourceCodeLine = 1349;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ISREGISTERING == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 1351;
                STARTHEARTBEAT (  __context__  ) ; 
                } 
            
            
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    
}

object DISCONNECT_OnPush_1 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 1359;
        CancelWait ( "STARTHEARTBEATPROCESS" ) ; 
        __context__.SourceCodeLine = 1361;
        RESET (  __context__  ) ; 
        __context__.SourceCodeLine = 1362;
        CancelWait ( "QUEUEFALSERESPONSE" ) ; 
        __context__.SourceCodeLine = 1364;
        Functions.SocketUDP_Disable ( SNMPAGENT ) ; 
        __context__.SourceCodeLine = 1365;
        ISCONNECTED = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1367;
        IS_COMMUNICATING  .Value = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1368;
        STOPHEARTBEAT (  __context__  ) ; 
        __context__.SourceCodeLine = 1370;
        Functions.ClearBuffer ( SNMPAGENT .  SocketRxBuf ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object SNMPAGENT_OnSocketReceive_2 ( Object __Info__ )

    { 
    SocketEventInfo __SocketInfo__ = (SocketEventInfo)__Info__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SocketInfo__);
        
        __context__.SourceCodeLine = 1376;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PARSINGDEVICEBUSY == 0))  ) ) 
            { 
            __context__.SourceCodeLine = 1378;
            PARSINGDEVICEBUSY = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 1380;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (TEST_SNMPMSGITEM( __context__ , SNMPAGENT.SocketRxBuf ) == 1))  ) ) 
                { 
                __context__.SourceCodeLine = 1382;
                PARSESTATUS = (ushort) ( DECODE_SNMPMSG( __context__ , SNMPAGENT.SocketRxBuf ) ) ; 
                __context__.SourceCodeLine = 1384;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (PARSEDSNMPMSG_PDU.ERROR == 0) ) && Functions.TestForTrue ( Functions.BoolToInt (PARSESTATUS == 1) )) ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 1387;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PARSEDSNMPMSG_PDU.MSGID == 0))  ) ) 
                        { 
                        __context__.SourceCodeLine = 1389;
                        GOODRESPONSE (  __context__  ) ; 
                        __context__.SourceCodeLine = 1391;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (IS_INITIALIZED  .Value == 0))  ) ) 
                            {
                            __context__.SourceCodeLine = 1392;
                            INITMODULES (  __context__  ) ; 
                            }
                        
                        } 
                    
                    else 
                        {
                        __context__.SourceCodeLine = 1395;
                        PACKMODULERESPONSE (  __context__ , PARSEDSNMPMSG_PDU, PARSEDSNMPMSG_VARBIND) ; 
                        }
                    
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 1400;
                    /* Trace( "ERROR: {0:d}", (int)PARSEDSNMPMSG_PDU.ERROR) */ ; 
                    __context__.SourceCodeLine = 1401;
                    GOODRESPONSE (  __context__  ) ; 
                    } 
                
                } 
            
            else 
                {
                __context__.SourceCodeLine = 1405;
                Functions.ClearBuffer ( SNMPAGENT .  SocketRxBuf ) ; 
                }
            
            __context__.SourceCodeLine = 1407;
            PARSINGDEVICEBUSY = (ushort) ( 0 ) ; 
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SocketInfo__ ); }
    return this;
    
}

object FROM_MODULES_OnChange_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 1413;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PARSINGMODULEBUSY == 0))  ) ) 
            { 
            __context__.SourceCodeLine = 1415;
            PARSINGMODULEBUSY = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 1417;
            while ( Functions.TestForTrue  ( ( 1)  ) ) 
                { 
                __context__.SourceCodeLine = 1419;
                PARSEDMODULEMSG  .UpdateValue ( Functions.Gather ( ">" , FROM_MODULES )  ) ; 
                __context__.SourceCodeLine = 1421;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( PARSEDMODULEMSG ) > 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 1424;
                    if ( Functions.TestForTrue  ( ( CONTAINS( __context__ , "GET" , PARSEDMODULEMSG ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 1426;
                        RESPONSEMODULEMSGID = (ushort) ( Functions.Atoi( GETBOUNDSTRING( __context__ , PARSEDMODULEMSG , "<" , "|" ) ) ) ; 
                        __context__.SourceCodeLine = 1428;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( RESPONSEMODULEMSGID > 0 ) ) && Functions.TestForTrue ( Functions.BoolToInt ( RESPONSEMODULEMSGID <= Functions.GetNumArrayCols( MODULECOMM ) ) )) ))  ) ) 
                            { 
                            __context__.SourceCodeLine = 1430;
                            PARSEDMODULEOID  .UpdateValue ( GETBOUNDSTRING (  __context__ , PARSEDMODULEMSG, "|", ">")  ) ; 
                            __context__.SourceCodeLine = 1431;
                            ENQUEUE (  __context__ , ENCODE_SNMPGET( __context__ , (ushort)( RESPONSEMODULEMSGID ) , PARSEDMODULEOID ), (ushort)( 0 )) ; 
                            } 
                        
                        } 
                    
                    else 
                        {
                        __context__.SourceCodeLine = 1435;
                        if ( Functions.TestForTrue  ( ( CONTAINS( __context__ , "SET" , PARSEDMODULEMSG ))  ) ) 
                            { 
                            __context__.SourceCodeLine = 1437;
                            RESPONSEMODULEMSGID = (ushort) ( Functions.Atoi( GETBOUNDSTRING( __context__ , PARSEDMODULEMSG , "<" , "|" ) ) ) ; 
                            __context__.SourceCodeLine = 1439;
                            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( RESPONSEMODULEMSGID > 0 ) ) && Functions.TestForTrue ( Functions.BoolToInt ( RESPONSEMODULEMSGID <= Functions.GetNumArrayCols( MODULECOMM ) ) )) ))  ) ) 
                                { 
                                __context__.SourceCodeLine = 1441;
                                PARSEDMODULETYPE  .UpdateValue ( GETBOUNDSTRING (  __context__ , PARSEDMODULEMSG, "|", "|")  ) ; 
                                __context__.SourceCodeLine = 1442;
                                TRASH  .UpdateValue ( Functions.Remove ( "|" , PARSEDMODULEMSG )  ) ; 
                                __context__.SourceCodeLine = 1444;
                                PARSEDMODULEOID  .UpdateValue ( GETBOUNDSTRING (  __context__ , PARSEDMODULEMSG, "|", "|")  ) ; 
                                __context__.SourceCodeLine = 1445;
                                TRASH  .UpdateValue ( Functions.Remove ( "|" , PARSEDMODULEMSG )  ) ; 
                                __context__.SourceCodeLine = 1447;
                                PARSEDMODULEVALUE  .UpdateValue ( GETBOUNDSTRING (  __context__ , PARSEDMODULEMSG, "|", ">")  ) ; 
                                __context__.SourceCodeLine = 1449;
                                BUILDSNMPMESSAGESET (  __context__ , (ushort)( RESPONSEMODULEMSGID ), PARSEDMODULETYPE, PARSEDMODULEOID, PARSEDMODULEVALUE) ; 
                                } 
                            
                            } 
                        
                        else 
                            {
                            __context__.SourceCodeLine = 1453;
                            if ( Functions.TestForTrue  ( ( CONTAINS( __context__ , "RESPONSE_OK" , PARSEDMODULEMSG ))  ) ) 
                                { 
                                __context__.SourceCodeLine = 1455;
                                RESPONSEMODULEMSGID = (ushort) ( Functions.Atoi( GETBOUNDSTRING( __context__ , PARSEDMODULEMSG , "<" , ">" ) ) ) ; 
                                __context__.SourceCodeLine = 1457;
                                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( RESPONSEMODULEMSGID > 0 ) ) && Functions.TestForTrue ( Functions.BoolToInt ( RESPONSEMODULEMSGID <= Functions.GetNumArrayCols( MODULECOMM ) ) )) ))  ) ) 
                                    { 
                                    __context__.SourceCodeLine = 1461;
                                    CreateWait ( "__SPLS_TMPVAR__WAITLABEL_1__" , 20 , __SPLS_TMPVAR__WAITLABEL_1___Callback ) ;
                                    } 
                                
                                } 
                            
                            else 
                                {
                                __context__.SourceCodeLine = 1466;
                                if ( Functions.TestForTrue  ( ( CONTAINS( __context__ , "INIT_DONE" , PARSEDMODULEMSG ))  ) ) 
                                    { 
                                    __context__.SourceCodeLine = 1468;
                                    ISINITIALIZING = (ushort) ( 0 ) ; 
                                    __context__.SourceCodeLine = 1470;
                                    RESPONSEMODULEMSGID = (ushort) ( Functions.Atoi( GETBOUNDSTRING( __context__ , PARSEDMODULEMSG , "<" , ">" ) ) ) ; 
                                    __context__.SourceCodeLine = 1472;
                                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( RESPONSEMODULEMSGID > 0 ) ) && Functions.TestForTrue ( Functions.BoolToInt ( RESPONSEMODULEMSGID <= Functions.GetNumArrayCols( MODULECOMM ) ) )) ))  ) ) 
                                        { 
                                        __context__.SourceCodeLine = 1474;
                                        MODULECOMM [ RESPONSEMODULEMSGID] . ISINITIALIZED = (ushort) ( 1 ) ; 
                                        } 
                                    
                                    __context__.SourceCodeLine = 1477;
                                    INITMODULES (  __context__  ) ; 
                                    } 
                                
                                else 
                                    {
                                    __context__.SourceCodeLine = 1480;
                                    if ( Functions.TestForTrue  ( ( CONTAINS( __context__ , "REGISTER" , PARSEDMODULEMSG ))  ) ) 
                                        { 
                                        __context__.SourceCodeLine = 1482;
                                        RESPONSEMODULEMSGID = (ushort) ( Functions.Atoi( GETBOUNDSTRING( __context__ , PARSEDMODULEMSG , "<" , ">" ) ) ) ; 
                                        __context__.SourceCodeLine = 1484;
                                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( RESPONSEMODULEMSGID > 0 ) ) && Functions.TestForTrue ( Functions.BoolToInt ( RESPONSEMODULEMSGID <= Functions.GetNumArrayCols( MODULECOMM ) ) )) ))  ) ) 
                                            { 
                                            __context__.SourceCodeLine = 1486;
                                            MODULECOMM [ RESPONSEMODULEMSGID] . ISREGISTERED = (ushort) ( 1 ) ; 
                                            } 
                                        
                                        } 
                                    
                                    }
                                
                                }
                            
                            }
                        
                        }
                    
                    } 
                
                __context__.SourceCodeLine = 1417;
                } 
            
            __context__.SourceCodeLine = 1492;
            PARSINGMODULEBUSY = (ushort) ( 0 ) ; 
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

public void __SPLS_TMPVAR__WAITLABEL_1___CallbackFn( object stateInfo )
{

    try
    {
        Wait __LocalWait__ = (Wait)stateInfo;
        SplusExecutionContext __context__ = SplusThreadStartCode(__LocalWait__);
        __LocalWait__.RemoveFromList();
        
            {
            __context__.SourceCodeLine = 1462;
            GOODRESPONSE (  __context__  ) ; 
            }
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    
}

public override object FunctionMain (  object __obj__ ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 1505;
        PARSINGDEVICEBUSY = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1506;
        PARSINGMODULEBUSY = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1508;
        IS_COMMUNICATING  .Value = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1510;
        ISCONNECTED = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1512;
        ISREGISTERING = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1514;
        PARSESTATUS = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 1516;
        RESET (  __context__  ) ; 
        __context__.SourceCodeLine = 1518;
        WaitForInitializationComplete ( ) ; 
        __context__.SourceCodeLine = 1519;
        STARTREGISTRATION (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
    _SplusNVRAM = new SplusNVRAM( this );
    PARSEDDEVICEMSG  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
    PARSEDMODULEMSG  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 256, this );
    PARSEDMODULETYPE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 256, this );
    PARSEDMODULEOID  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 256, this );
    PARSEDMODULEVALUE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 256, this );
    TRASH  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
    SCOMMANDQUEUE  = new CrestronString[ 51 ];
    for( uint i = 0; i < 51; i++ )
        SCOMMANDQUEUE [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
    SSTATUSQUEUE  = new CrestronString[ 51 ];
    for( uint i = 0; i < 51; i++ )
        SSTATUSQUEUE [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, this );
    SNMPAGENT  = new SplusUdpSocket ( 1024, this );
    QUEUECOMM  = new _COMMQUEUE( this, true );
    QUEUECOMM .PopulateCustomAttributeList( false );
    PARSEDSNMPMSG  = new _SNMPMSG( this, true );
    PARSEDSNMPMSG .PopulateCustomAttributeList( false );
    PARSEDSNMPMSG_PDU  = new _SNMPMSG_PDU( this, true );
    PARSEDSNMPMSG_PDU .PopulateCustomAttributeList( false );
    PARSEDSNMPMSG_VARBIND  = new _SNMPMSG_VARBIND( this, true );
    PARSEDSNMPMSG_VARBIND .PopulateCustomAttributeList( false );
    PARSEDSNMPMSGITEM  = new _SNMPMSGITEM( this, true );
    PARSEDSNMPMSGITEM .PopulateCustomAttributeList( false );
    MODULECOMM  = new _MODULECOMM[ 51 ];
    for( uint i = 0; i < 51; i++ )
    {
        MODULECOMM [i] = new _MODULECOMM( this, true );
        MODULECOMM [i].PopulateCustomAttributeList( false );
        
    }
    
    CONNECT = new Crestron.Logos.SplusObjects.DigitalInput( CONNECT__DigitalInput__, this );
    m_DigitalInputList.Add( CONNECT__DigitalInput__, CONNECT );
    
    DISCONNECT = new Crestron.Logos.SplusObjects.DigitalInput( DISCONNECT__DigitalInput__, this );
    m_DigitalInputList.Add( DISCONNECT__DigitalInput__, DISCONNECT );
    
    IS_COMMUNICATING = new Crestron.Logos.SplusObjects.DigitalOutput( IS_COMMUNICATING__DigitalOutput__, this );
    m_DigitalOutputList.Add( IS_COMMUNICATING__DigitalOutput__, IS_COMMUNICATING );
    
    IS_INITIALIZED = new Crestron.Logos.SplusObjects.DigitalOutput( IS_INITIALIZED__DigitalOutput__, this );
    m_DigitalOutputList.Add( IS_INITIALIZED__DigitalOutput__, IS_INITIALIZED );
    
    TO_MODULES = new InOutArray<StringOutput>( 50, this );
    for( uint i = 0; i < 50; i++ )
    {
        TO_MODULES[i+1] = new Crestron.Logos.SplusObjects.StringOutput( TO_MODULES__AnalogSerialOutput__ + i, this );
        m_StringOutputList.Add( TO_MODULES__AnalogSerialOutput__ + i, TO_MODULES[i+1] );
    }
    
    FROM_MODULES = new Crestron.Logos.SplusObjects.BufferInput( FROM_MODULES__AnalogSerialInput__, 50000, this );
    m_StringInputList.Add( FROM_MODULES__AnalogSerialInput__, FROM_MODULES );
    
    PROTOCOL = new UShortParameter( PROTOCOL__Parameter__, this );
    m_ParameterList.Add( PROTOCOL__Parameter__, PROTOCOL );
    
    COMMUNITY = new StringParameter( COMMUNITY__Parameter__, this );
    m_ParameterList.Add( COMMUNITY__Parameter__, COMMUNITY );
    
    IPADDRESS = new StringParameter( IPADDRESS__Parameter__, this );
    m_ParameterList.Add( IPADDRESS__Parameter__, IPADDRESS );
    
    QUEUEFALSERESPONSE_Callback = new WaitFunction( QUEUEFALSERESPONSE_CallbackFn );
    HEARTBEAT_Callback = new WaitFunction( HEARTBEAT_CallbackFn );
    REGISTRATIONWAIT_Callback = new WaitFunction( REGISTRATIONWAIT_CallbackFn );
    STARTHEARTBEATPROCESS_Callback = new WaitFunction( STARTHEARTBEATPROCESS_CallbackFn );
    __SPLS_TMPVAR__WAITLABEL_1___Callback = new WaitFunction( __SPLS_TMPVAR__WAITLABEL_1___CallbackFn );
    
    CONNECT.OnDigitalPush.Add( new InputChangeHandlerWrapper( CONNECT_OnPush_0, false ) );
    DISCONNECT.OnDigitalPush.Add( new InputChangeHandlerWrapper( DISCONNECT_OnPush_1, false ) );
    SNMPAGENT.OnSocketReceive.Add( new SocketHandlerWrapper( SNMPAGENT_OnSocketReceive_2, false ) );
    FROM_MODULES.OnSerialChange.Add( new InputChangeHandlerWrapper( FROM_MODULES_OnChange_3, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_SNMP_CLIENT_PROCESSOR_MODULE_V1_1 ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}


private WaitFunction QUEUEFALSERESPONSE_Callback;
private WaitFunction HEARTBEAT_Callback;
private WaitFunction REGISTRATIONWAIT_Callback;
private WaitFunction STARTHEARTBEATPROCESS_Callback;
private WaitFunction __SPLS_TMPVAR__WAITLABEL_1___Callback;


const uint CONNECT__DigitalInput__ = 0;
const uint DISCONNECT__DigitalInput__ = 1;
const uint FROM_MODULES__AnalogSerialInput__ = 0;
const uint IS_COMMUNICATING__DigitalOutput__ = 0;
const uint IS_INITIALIZED__DigitalOutput__ = 1;
const uint TO_MODULES__AnalogSerialOutput__ = 0;
const uint PROTOCOL__Parameter__ = 10;
const uint COMMUNITY__Parameter__ = 11;
const uint IPADDRESS__Parameter__ = 12;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}

[SplusStructAttribute(-1, true, false)]
public class _SNMPMSG_VARBIND : SplusStructureBase
{

    [SplusStructAttribute(0, false, false)]
    public ushort  TYPE = 0;
    
    [SplusStructAttribute(1, false, false)]
    public CrestronString  SVALUE;
    
    [SplusStructAttribute(2, false, false)]
    public uint  NVALUE = 0;
    
    [SplusStructAttribute(3, false, false)]
    public int  SNVALUE = 0;
    
    [SplusStructAttribute(4, false, false)]
    public CrestronString  OID;
    
    
    public _SNMPMSG_VARBIND( SplusObject __caller__, bool bIsStructureVolatile ) : base ( __caller__, bIsStructureVolatile )
    {
        SVALUE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 128, Owner );
        OID  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 128, Owner );
        
        
    }
    
}
[SplusStructAttribute(-1, true, false)]
public class _SNMPMSG_PDU : SplusStructureBase
{

    [SplusStructAttribute(0, false, false)]
    public uint  MSGID = 0;
    
    [SplusStructAttribute(1, false, false)]
    public uint  ERROR = 0;
    
    [SplusStructAttribute(2, false, false)]
    public uint  ERRORINDEX = 0;
    
    
    public _SNMPMSG_PDU( SplusObject __caller__, bool bIsStructureVolatile ) : base ( __caller__, bIsStructureVolatile )
    {
        
        
    }
    
}
[SplusStructAttribute(-1, true, false)]
public class _SNMPMSG : SplusStructureBase
{

    [SplusStructAttribute(0, false, false)]
    public uint  VERSION = 0;
    
    [SplusStructAttribute(1, false, false)]
    public CrestronString  COMMUNITY;
    
    
    public _SNMPMSG( SplusObject __caller__, bool bIsStructureVolatile ) : base ( __caller__, bIsStructureVolatile )
    {
        COMMUNITY  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 50, Owner );
        
        
    }
    
}
[SplusStructAttribute(-1, true, false)]
public class _SNMPMSGITEM : SplusStructureBase
{

    [SplusStructAttribute(0, false, false)]
    public ushort  TAG = 0;
    
    [SplusStructAttribute(1, false, false)]
    public ushort  LENGTH = 0;
    
    [SplusStructAttribute(2, false, false)]
    public CrestronString  DATA;
    
    
    public _SNMPMSGITEM( SplusObject __caller__, bool bIsStructureVolatile ) : base ( __caller__, bIsStructureVolatile )
    {
        DATA  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, Owner );
        
        
    }
    
}
[SplusStructAttribute(-1, true, false)]
public class _MODULECOMM : SplusStructureBase
{

    [SplusStructAttribute(0, false, false)]
    public ushort  ISREGISTERED = 0;
    
    [SplusStructAttribute(1, false, false)]
    public ushort  ISINITIALIZED = 0;
    
    
    public _MODULECOMM( SplusObject __caller__, bool bIsStructureVolatile ) : base ( __caller__, bIsStructureVolatile )
    {
        
        
    }
    
}
[SplusStructAttribute(-1, true, false)]
public class _COMMQUEUE : SplusStructureBase
{

    [SplusStructAttribute(0, false, false)]
    public ushort  NBUSY = 0;
    
    [SplusStructAttribute(1, false, false)]
    public ushort  NHASITEMS = 0;
    
    [SplusStructAttribute(2, false, false)]
    public ushort  NCOMMANDHEAD = 0;
    
    [SplusStructAttribute(3, false, false)]
    public ushort  NCOMMANDTAIL = 0;
    
    [SplusStructAttribute(4, false, false)]
    public ushort  NSTATUSHEAD = 0;
    
    [SplusStructAttribute(5, false, false)]
    public ushort  NSTATUSTAIL = 0;
    
    [SplusStructAttribute(6, false, false)]
    public ushort  NSTRIKECOUNT = 0;
    
    [SplusStructAttribute(7, false, false)]
    public ushort  NRESENDLAST = 0;
    
    [SplusStructAttribute(8, false, false)]
    public CrestronString  SLASTMESSAGE;
    
    
    public _COMMQUEUE( SplusObject __caller__, bool bIsStructureVolatile ) : base ( __caller__, bIsStructureVolatile )
    {
        SLASTMESSAGE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 512, Owner );
        
        
    }
    
}

}
