﻿//扩展easyui表单的验证
$.extend($.fn.validatebox.defaults.rules, {
    //验证汉子
    CHS: {
        validator: function (value) {
            return /^[\u0391-\uFFE5]+$/.test(value);
        },
        message: '只能输入汉字'
    },
    //移动手机号码验证
    mobile: {//value值为文本框中的值
        validator: function (value) {
            var reg = /^1[3|4|5|8|9]\d{9}$/;
            return reg.test(value);
        },
        message: '输入手机号码格式不准确.'
    },
    //国内邮编验证
    zipcode: {
        validator: function (value) {
            var reg = /^[1-9]\d{5}$/;
            return reg.test(value);
        },
        message: '邮编必须是非0开始的6位数字.'
    },
    //用户账号验证(只能包括 _ 数字 字母) 
    account: {//param的值为[]中值
        validator: function (value, param) {
            if (value.length < param[0] || value.length > param[1]) {
                $.fn.validatebox.defaults.rules.account.message = '用户名长度必须在' + param[0] + '至' + param[1] + '范围';
                return false;
            } else {
                if (!/^[\w]+$/.test(value)) {
                    $.fn.validatebox.defaults.rules.account.message = '用户名只能数字、字母、下划线组成.';
                    return false;
                } else {
                    return true;
                }
            }
        }, message: ''
    },
    //存货分类编码验证，产品类别编码，中间以~字母分割，前后必须是数字,或字母开头数字结尾
    ProductClassCode: {
        validator: function (value, param) {
            var idx = value.indexOf('~');
            if (idx == -1) {
                $.fn.validatebox.defaults.rules.ProductClassCode.message = '中间以~字符分割';
                return false;
            }
            var arr = value.split('~');
            var reg = /^[a-zA-Z0-9]+[0-9]$/;
            if (!reg.test(arr[0]) || !reg.test(arr[1])) {
                $.fn.validatebox.defaults.rules.ProductClassCode.message = '必须以字母或数字开头，数字结尾。';
                return false
            }
            return true;
        }, message: ''
    },
    //非负整数
    NonnegativeInteger: {
        validator: function (value, param) {
            var reg = /^[1-9]d*|0$/;
            if (!reg.test(value)) {
                $.fn.validatebox.defaults.rules.NonnegativeInteger.message = '请输入非负整数';
                return false
            }
            return true;
        }, message: ''
    },
    //存货编码
     InvCode: {
        validator: function (value, param) {
            var reg = /^[a-zA-Z0-9]+[0-9]$/;
            if (!reg.test(value)) {
                $.fn.validatebox.defaults.rules.InvCode.message = '必须以字母或数字开头，数字结尾。';
                return false
            }
            return true;
        }, message: ''
    },
    //数字
    Digit: {
        validator: function (value, param) {
            var reg = /^(0|[1-9]\d*)$|^(0|[1-9]\d*)\.(\d+)$/;
            if (!reg.test(value)) {
                $.fn.validatebox.defaults.rules.Digit.message = '请输入数字';
                return false
            }
            return true;
        }, message: ''
    }
})