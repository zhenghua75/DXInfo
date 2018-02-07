

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0555 */
/* at Mon Jul 27 11:01:18 2015
 */
/* Compiler settings for DXInfoActiveX.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 7.00.0555 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __DXInfoActiveX_i_h__
#define __DXInfoActiveX_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ICard_FWD_DEFINED__
#define __ICard_FWD_DEFINED__
typedef interface ICard ICard;
#endif 	/* __ICard_FWD_DEFINED__ */


#ifndef __IKey_FWD_DEFINED__
#define __IKey_FWD_DEFINED__
typedef interface IKey IKey;
#endif 	/* __IKey_FWD_DEFINED__ */


#ifndef __Card_FWD_DEFINED__
#define __Card_FWD_DEFINED__

#ifdef __cplusplus
typedef class Card Card;
#else
typedef struct Card Card;
#endif /* __cplusplus */

#endif 	/* __Card_FWD_DEFINED__ */


#ifndef __Key_FWD_DEFINED__
#define __Key_FWD_DEFINED__

#ifdef __cplusplus
typedef class Key Key;
#else
typedef struct Key Key;
#endif /* __cplusplus */

#endif 	/* __Key_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __ICard_INTERFACE_DEFINED__
#define __ICard_INTERFACE_DEFINED__

/* interface ICard */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ICard;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("674D940C-6604-4062-9396-5B7855B809CB")
    ICard : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE ReadCard( 
            /* [retval][out] */ BSTR *data) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE PutCard( 
            /* [in] */ BSTR data,
            /* [retval][out] */ VARIANT_BOOL *issuc) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE RecycleCard( 
            /* [retval][out] */ VARIANT_BOOL *issuc) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ICardVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ICard * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ICard * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ICard * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ICard * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ICard * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ICard * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ICard * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *ReadCard )( 
            ICard * This,
            /* [retval][out] */ BSTR *data);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *PutCard )( 
            ICard * This,
            /* [in] */ BSTR data,
            /* [retval][out] */ VARIANT_BOOL *issuc);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *RecycleCard )( 
            ICard * This,
            /* [retval][out] */ VARIANT_BOOL *issuc);
        
        END_INTERFACE
    } ICardVtbl;

    interface ICard
    {
        CONST_VTBL struct ICardVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICard_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ICard_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ICard_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ICard_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define ICard_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define ICard_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define ICard_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define ICard_ReadCard(This,data)	\
    ( (This)->lpVtbl -> ReadCard(This,data) ) 

#define ICard_PutCard(This,data,issuc)	\
    ( (This)->lpVtbl -> PutCard(This,data,issuc) ) 

#define ICard_RecycleCard(This,issuc)	\
    ( (This)->lpVtbl -> RecycleCard(This,issuc) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ICard_INTERFACE_DEFINED__ */


#ifndef __IKey_INTERFACE_DEFINED__
#define __IKey_INTERFACE_DEFINED__

/* interface IKey */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IKey;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("3E5BEA52-378C-4918-9FE3-168BA37C893B")
    IKey : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Verify( 
            /* [retval][out] */ VARIANT_BOOL *issuc) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE GetHardwareID( 
            /* [retval][out] */ BSTR *hdId) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE GetKeyNo( 
            /* [retval][out] */ BSTR *data) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IKeyVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IKey * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IKey * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IKey * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IKey * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IKey * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IKey * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IKey * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Verify )( 
            IKey * This,
            /* [retval][out] */ VARIANT_BOOL *issuc);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *GetHardwareID )( 
            IKey * This,
            /* [retval][out] */ BSTR *hdId);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *GetKeyNo )( 
            IKey * This,
            /* [retval][out] */ BSTR *data);
        
        END_INTERFACE
    } IKeyVtbl;

    interface IKey
    {
        CONST_VTBL struct IKeyVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IKey_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IKey_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IKey_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IKey_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IKey_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IKey_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IKey_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IKey_Verify(This,issuc)	\
    ( (This)->lpVtbl -> Verify(This,issuc) ) 

#define IKey_GetHardwareID(This,hdId)	\
    ( (This)->lpVtbl -> GetHardwareID(This,hdId) ) 

#define IKey_GetKeyNo(This,data)	\
    ( (This)->lpVtbl -> GetKeyNo(This,data) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IKey_INTERFACE_DEFINED__ */



#ifndef __DXInfoActiveXLib_LIBRARY_DEFINED__
#define __DXInfoActiveXLib_LIBRARY_DEFINED__

/* library DXInfoActiveXLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_DXInfoActiveXLib;

EXTERN_C const CLSID CLSID_Card;

#ifdef __cplusplus

class DECLSPEC_UUID("195FD03D-EC38-4F72-B3BA-E5F243E404F5")
Card;
#endif

EXTERN_C const CLSID CLSID_Key;

#ifdef __cplusplus

class DECLSPEC_UUID("01A8EC23-6702-476C-AF5F-E934C71D0FE6")
Key;
#endif
#endif /* __DXInfoActiveXLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


