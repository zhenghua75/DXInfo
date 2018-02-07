using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web.Routing;
using DXInfo.Models;
using System.Web.Security;
using DXInfo.Data.Contracts;
using Ninject;
using System.Web.Mvc;

namespace DXInfo.Sitemap
{
    public class MvcSQLSitemapProvider : StaticSiteMapProvider
    {
        private SiteMapNode _root = null;

        private Dictionary<string, SiteMapNode> _nodes = new Dictionary<string, SiteMapNode>();
        public override void Initialize(string name, NameValueCollection attributes)
        {
            if (attributes == null)
                throw new ArgumentNullException("attributes");
            if (string.IsNullOrEmpty(name))
                name = "MvcSitemapProvider";
            if (string.IsNullOrEmpty(attributes["description"]))
            {
                attributes.Remove("description");
                attributes.Add("description", "MVC site map provider");
            }
            base.Initialize(name, attributes);
            if (attributes.Count > 0)
            {
                string attr = attributes.GetKey(0);
                if (!string.IsNullOrEmpty(attr))
                    throw new ProviderException(string.Format("Unrecognized attribute: {0}", attr));
            }
        }

        public override SiteMapNode BuildSiteMap()
        {
            lock (this)
            {
                if (_root != null)
                {
                    return _root;
                }
                var Uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
                
                    var siteMapQuery = (from s in Uow.aspnet_Sitemaps.GetAll()
                                       where s.IsMenu == true && s.IsClient == false
                                       orderby s.Sort
                                       select s).ToList();

                    var firstNode = (from s in siteMapQuery
                                where s.IsMenu == true && s.IsClient == false && s.Code==s.ParentCode select s).FirstOrDefault();
                if (firstNode == null)
                {
                    throw new ProviderException("无根节点");
                }
                _root = CreateSiteMapFromRow(firstNode);
                AddNode(_root, null);

                CreateSiteMap(siteMapQuery,firstNode.Code);
                //foreach (var item in siteMpaQuery)
                //{
                //    if (!_nodes.ContainsKey(item.Code))
                //    {
                //        if (item.Code == item.ParentCode)
                //        {
                //            _root = CreateSiteMapFromRow(item);
                //            AddNode(_root, null);
                //        }
                //        else
                //        {
                //            SiteMapNode node = CreateSiteMapFromRow(item);
                //            AddNode(node, GetParentNodeFromNode(item));
                //        }
                //    }
                //}
                //_nodes = _nodes.OrderBy(o => o.Key).ToDictionary(d=>d.Key,d=>d.Value);
                return _root;
            }
        }

        private void CreateSiteMap(List<DXInfo.Models.aspnet_Sitemaps> siteMapQuery,string code)
        {
            var siteMap = (from s in siteMapQuery
                           where s.IsMenu == true && s.IsClient == false && s.ParentCode == code && s.Code != code
                           orderby s.Sort
                           select s).ToList();
            if (siteMap != null && siteMap.Count > 0)
            {
                foreach (var item in siteMap)
                {
                    SiteMapNode node = CreateSiteMapFromRow(item);
                    AddNode(node, GetParentNodeFromNode(item));
                    CreateSiteMap(siteMapQuery,item.Code);
                }
            }
        }
        private SiteMapNode CreateSiteMapFromRow(DXInfo.Models.aspnet_Sitemaps item)
        {
            if (_nodes.ContainsKey(item.Code))
                throw new ProviderException(string.Format("重复节点Code={0},Title={1}", item.Code, item.Title));
            SiteMapNode node = new SiteMapNode(this, item.Code);
            node["IsAuthorize"] = item.IsAuthorize.ToString();
            if (!string.IsNullOrEmpty(item.Url))
            {
                node.Title = string.IsNullOrEmpty(item.Title) ? null : item.Title;
                node.Description = string.IsNullOrEmpty(item.Description) ? null : item.Description;
                node.Url = string.IsNullOrEmpty(item.Url) ? null : item.Url;             
            }
            else
            {
                node.Title = string.IsNullOrEmpty(item.Title) ? null : item.Title;
                node.Description = string.IsNullOrEmpty(item.Description) ? null : item.Description;

                //IDictionary<string, object> routeValues = new Dictionary<string, object>();

                //if (string.IsNullOrEmpty(item.Controller))
                //    routeValues.Add("controller", "Home");
                //else
                //    routeValues.Add("controller", item.Controller);

                //if (string.IsNullOrEmpty(item.Action))
                //    routeValues.Add("action", "Index");
                //else
                //    routeValues.Add("action", item.Action);

                //if (!string.IsNullOrEmpty(item.ParaId))
                //    routeValues.Add("id", item.ParaId);

               

                HttpContextWrapper httpContext = new HttpContextWrapper(HttpContext.Current);
                RouteData routeData = RouteTable.Routes.GetRouteData(httpContext);
                
                if (routeData != null)
                {
                    //VirtualPathData virtualPath = routeData.Route.GetVirtualPath(new RequestContext(httpContext, routeData), new RouteValueDictionary(routeValues));
                    //string str = System.Web.Mvc.UrlHelper.GenerateUrl("", "", "", new RouteValueDictionary(routeValues), new RouteCollection(), httpContext, true);
                    System.Web.Routing.RequestContext rc = new RequestContext(httpContext, routeData);
                    System.Web.Mvc.UrlHelper url = new System.Web.Mvc.UrlHelper(rc);
                    RouteValueDictionary routeValues = new RouteValueDictionary();
                     
                    if (!string.IsNullOrEmpty(item.ParaId))
                    {
                        string[] paras = item.ParaId.Split('&');
                        foreach (string para in paras)
                        {
                            if (!string.IsNullOrEmpty(para))
                            {
                                string[] para1 = para.Split('=');
                                if (para1.Length == 2)
                                {
                                    routeValues.Add(para1[0], para1[1]);
                                }
                            }
                        }
                    }
                    node.Url = url.Action(item.Action, item.Controller, routeValues);
                    
                    //if (virtualPath != null)
                    //{
                    //    node.Url = "~/" + virtualPath.VirtualPath;
                    //}
                }
            }

            _nodes.Add(item.Code, node);

            return node;
        }

        private SiteMapNode GetParentNodeFromNode(DXInfo.Models.aspnet_Sitemaps item)
        {
            if (!_nodes.ContainsKey(item.ParentCode))
            {
                throw new ProviderException(string.Format("无效父节点Code={0},Title={1}", item.Code, item.Title));
                //DXInfo.Models.aspnet_Sitemaps item1 = siteMpaQuery.Find(f => f.Code == item.ParentCode);
                //SiteMapNode node = CreateSiteMapFromRow(item1);
                //AddNode(node, GetParentNodeFromNode(item1));
            }
            return _nodes[item.ParentCode];
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            return BuildSiteMap();
        }
    }
}
