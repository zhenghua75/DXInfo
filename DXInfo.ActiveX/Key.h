// Key.h : CKey ������
#pragma once
#include "resource.h"       // ������
#include <atlctl.h>
#include "DXInfoActiveX_i.h"

#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Windows CE ƽ̨(�粻�ṩ��ȫ DCOM ֧�ֵ� Windows Mobile ƽ̨)���޷���ȷ֧�ֵ��߳� COM ���󡣶��� _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA ��ǿ�� ATL ֧�ִ������߳� COM ����ʵ�ֲ�����ʹ���䵥�߳� COM ����ʵ�֡�rgs �ļ��е��߳�ģ���ѱ�����Ϊ��Free����ԭ���Ǹ�ģ���Ƿ� DCOM Windows CE ƽ̨֧�ֵ�Ψһ�߳�ģ�͡�"
#endif

using namespace ATL;



// CKey
class ATL_NO_VTABLE CKey :
	public CComObjectRootEx<CComSingleThreadModel>,
	public IDispatchImpl<IKey, &IID_IKey, &LIBID_DXInfoActiveXLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
	public IOleControlImpl<CKey>,
	public IOleObjectImpl<CKey>,
	public IOleInPlaceActiveObjectImpl<CKey>,
	public IViewObjectExImpl<CKey>,
	public IOleInPlaceObjectWindowlessImpl<CKey>,
	public ISupportErrorInfo,
	public IQuickActivateImpl<CKey>,
#ifndef _WIN32_WCE
	public IDataObjectImpl<CKey>,
#endif
	public IProvideClassInfo2Impl<&CLSID_Key, NULL, &LIBID_DXInfoActiveXLib>,
	public IObjectSafetyImpl<CKey, INTERFACESAFE_FOR_UNTRUSTED_CALLER>,
	//public IObjectSafetyImpl<CKey, INTERFACESAFE_FOR_UNTRUSTED_CALLER| INTERFACESAFE_FOR_UNTRUSTED_DATA>,
	public CComCoClass<CKey, &CLSID_Key>,
	public CComControl<CKey>
{
public:


	CKey()
	{
	}

DECLARE_OLEMISC_STATUS(OLEMISC_RECOMPOSEONRESIZE |
	OLEMISC_INVISIBLEATRUNTIME |
	OLEMISC_CANTLINKINSIDE |
	OLEMISC_INSIDEOUT |
	OLEMISC_ACTIVATEWHENVISIBLE |
	OLEMISC_SETCLIENTSITEFIRST
)

DECLARE_REGISTRY_RESOURCEID(IDR_KEY)


BEGIN_COM_MAP(CKey)
	COM_INTERFACE_ENTRY(IKey)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(IViewObjectEx)
	COM_INTERFACE_ENTRY(IViewObject2)
	COM_INTERFACE_ENTRY(IViewObject)
	COM_INTERFACE_ENTRY(IOleInPlaceObjectWindowless)
	COM_INTERFACE_ENTRY(IOleInPlaceObject)
	COM_INTERFACE_ENTRY2(IOleWindow, IOleInPlaceObjectWindowless)
	COM_INTERFACE_ENTRY(IOleInPlaceActiveObject)
	COM_INTERFACE_ENTRY(IOleControl)
	COM_INTERFACE_ENTRY(IOleObject)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
	COM_INTERFACE_ENTRY(IQuickActivate)
#ifndef _WIN32_WCE
	COM_INTERFACE_ENTRY(IDataObject)
#endif
	COM_INTERFACE_ENTRY(IProvideClassInfo)
	COM_INTERFACE_ENTRY(IProvideClassInfo2)
	COM_INTERFACE_ENTRY_IID(IID_IObjectSafety, IObjectSafety)
	//COM_INTERFACE_ENTRY(IObjectSafety)
END_COM_MAP()

BEGIN_PROP_MAP(CKey)
	PROP_DATA_ENTRY("_cx", m_sizeExtent.cx, VT_UI4)
	PROP_DATA_ENTRY("_cy", m_sizeExtent.cy, VT_UI4)
	// ʾ����
	// PROP_ENTRY_TYPE("������", dispid, clsid, vtType)
	// PROP_PAGE(CLSID_StockColorPage)
END_PROP_MAP()


BEGIN_MSG_MAP(CKey)
	CHAIN_MSG_MAP(CComControl<CKey>)
	DEFAULT_REFLECTION_HANDLER()
END_MSG_MAP()
// �������ԭ��:
//  LRESULT MessageHandler(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
//  LRESULT CommandHandler(WORD wNotifyCode, WORD wID, HWND hWndCtl, BOOL& bHandled);
//  LRESULT NotifyHandler(int idCtrl, LPNMHDR pnmh, BOOL& bHandled);

// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid)
	{
		static const IID* const arr[] =
		{
			&IID_IKey,
		};

		for (int i=0; i<sizeof(arr)/sizeof(arr[0]); i++)
		{
			if (InlineIsEqualGUID(*arr[i], riid))
				return S_OK;
		}
		return S_FALSE;
	}

// IViewObjectEx
	DECLARE_VIEW_STATUS(VIEWSTATUS_SOLIDBKGND | VIEWSTATUS_OPAQUE)

// IKey
public:
	HRESULT OnDraw(ATL_DRAWINFO& di)
	{
		RECT& rc = *(RECT*)di.prcBounds;
		// ��������������Ϊ di.prcBounds ָ���ľ���
		HRGN hRgnOld = NULL;
		if (GetClipRgn(di.hdcDraw, hRgnOld) != 1)
			hRgnOld = NULL;
		bool bSelectOldRgn = false;

		HRGN hRgnNew = CreateRectRgn(rc.left, rc.top, rc.right, rc.bottom);

		if (hRgnNew != NULL)
		{
			bSelectOldRgn = (SelectClipRgn(di.hdcDraw, hRgnNew) != ERROR);
		}

		Rectangle(di.hdcDraw, rc.left, rc.top, rc.right, rc.bottom);
		SetTextAlign(di.hdcDraw, TA_CENTER|TA_BASELINE);
		LPCTSTR pszText = _T("Key");
#ifndef _WIN32_WCE
		TextOut(di.hdcDraw,
			(rc.left + rc.right) / 2,
			(rc.top + rc.bottom) / 2,
			pszText,
			lstrlen(pszText));
#else
		ExtTextOut(di.hdcDraw,
			(rc.left + rc.right) / 2,
			(rc.top + rc.bottom) / 2,
			ETO_OPAQUE,
			NULL,
			pszText,
			ATL::lstrlen(pszText),
			NULL);
#endif

		if (bSelectOldRgn)
			SelectClipRgn(di.hdcDraw, hRgnOld);

		return S_OK;
	}


	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}
	STDMETHOD(Verify)(VARIANT_BOOL* issuc);
	STDMETHOD(GetHardwareID)(BSTR* hdId);
	STDMETHOD(GetKeyNo)(BSTR* data);

    /*STDMETHOD(GetInterfaceSafetyOptions)(REFIID riid,DWORD *pdwSupportedOptions,DWORD *pdwEnabledOptions);
    STDMETHOD(SetInterfaceSafetyOptions)(REFIID riid,DWORD dwOptionSetMask,DWORD dwEnabledOptions);*/
};

OBJECT_ENTRY_AUTO(__uuidof(Key), CKey)
