// dllmain.h : 模块类的声明。

class CDXInfoActiveXModule : public ATL::CAtlDllModuleT< CDXInfoActiveXModule >
{
public :
	DECLARE_LIBID(LIBID_DXInfoActiveXLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_DXINFOACTIVEX, "{DE0894D0-2C03-46AA-9C23-3F2C2B6EF650}")
};

extern class CDXInfoActiveXModule _AtlModule;
