/**
 * name: tm.pagination
 * Version: 1.0.0 beta
 */
angular.module('tm.pagination', []).directive('tmPagination',[function(){
    return {
        restrict: 'EA',
        template: '<div class="page-list">' +
            //'<div class="page-total" ng-show="conf.totalItems >= 0">' +
            '<div class="page-total">' +
            '每页<select ng-model="conf.itemsPerPage" ng-options="option for option in conf.perPageOptions " ng-change="changeItemsPerPage()"></select>' +
            '/共<strong>{{ conf.totalItems }}</strong>条 ' +
            '<span ng-show="conf.showJump">跳转至<input type="text" ng-model="jumpPageNum" ng-keyup="jumpPageKeyUp($event)"/></span>' +
            '<a ng-click="refresh()" class="pagination_refresh"></a>' +
            '</div>' +
            //'<div class="no-items" ng-show="conf.totalItems <= 0">暂无数据</div>' +
            //'<ul class="pagination" ng-show="conf.totalItems > 0">' +
            '<ul class="pagination">' +
            '<li ng-class="{disabled: conf.currentPage == 1}" ng-click="prevPage()"><span>&laquo;</span></li>' +
            '<li ng-repeat="item in pageList track by $index" ng-class="{active: item == conf.currentPage, separate: item == \'...\'}" ' +
            'ng-click="changeCurrentPage(item)">' +
            '<span>{{ item }}</span>' +
            '</li>' +
            '<li ng-class="{disabled: conf.currentPage == conf.numberOfPages}" ng-click="nextPage()"><span>&raquo;</span></li>' +
            '</ul>' +
            '</div>',
        replace: true,
        scope: {
            conf: '='
        },
        link: function(scope, element, attrs) {
            var conf = scope.conf;
            // 默认分页长度（即可用于点击的按钮数）
            var defaultPagesLength = 9;
            // 默认分页选项可调整每页显示的条数
            var defaultPerPageOptions = [10, 15, 20, 30, 50];
            // 默认每页的个数
            var defaultPerPage = 15;
            // 获取分页长度
            if(conf.pagesLength) {
                // 判断一下分页长度
                conf.pagesLength = parseInt(conf.pagesLength, 10);
                if(!conf.pagesLength) {
                    conf.pagesLength = defaultPagesLength;
                }
                // 分页长度必须为奇数，如果传偶数时，自动处理
                if(conf.pagesLength % 2 === 0) {
                    conf.pagesLength += 1;
                }
            } else {
                conf.pagesLength = defaultPagesLength
            }

            // 分页选项可调整每页显示的条数
            if(!conf.perPageOptions){
                //conf.perPageOptions = defaultPagesLength;
                conf.perPageOptions = defaultPerPageOptions;
            }

            // pageList数组
            function getPagination() {
                if(conf.currentPage) {
                    //conf.currentPage = parseInt(scope.conf.currentPage, 10);
                    conf.currentPage = parseInt(conf.currentPage, 10);
                }
                if(!conf.currentPage) {
                    conf.currentPage = 1;
                }

                if(conf.totalItems) {
                    conf.totalItems = parseInt(conf.totalItems, 10);
                }

                if(!conf.totalItems) {
                    conf.totalItems = 0;
                    //return;
                }
                
                if(conf.itemsPerPage) {
                    conf.itemsPerPage = parseInt(conf.itemsPerPage, 10);
                }
                if(!conf.itemsPerPage) {
                    conf.itemsPerPage = defaultPerPage;
                }

                conf.numberOfPages = Math.ceil(conf.totalItems / conf.itemsPerPage);  //计算总页数
                if (conf.numberOfPages === 0) {
                    conf.numberOfPages = 1;
                }

                // 如果分页总数>0，并且当前页大于分页总数，则当前页调整为分页总数，即最后一页
                if (conf.currentPage > conf.numberOfPages) {  
                    conf.currentPage = conf.numberOfPages;
                    conf.onChange();
                }

                // 如果itemsPerPage在不在perPageOptions数组中，就把itemsPerPage加入这个数组中
                var perPageOptionsLength = conf.perPageOptions.length;

                // 定义状态
                var perPageOptionsStatus;
                for(var i = 0; i < perPageOptionsLength; i++){
                    if(conf.perPageOptions[i] == conf.itemsPerPage){
                        perPageOptionsStatus = true;
                        break;
                    }
                }
                // 如果itemsPerPage在不在perPageOptions数组中，就把itemsPerPage加入这个数组中
                if(!perPageOptionsStatus){
                    conf.perPageOptions.push(conf.itemsPerPage);
                }

                // 对选项进行sort
                conf.perPageOptions.sort(function(a, b) {return a - b});

                // 页码相关
                var pageList = [];
                if(conf.numberOfPages <= conf.pagesLength){
                    // 判断总页数如果小于等于分页的长度，若小于则直接显示
                    for(i =1; i <= conf.numberOfPages; i++){
                        pageList.push(i);
                    }
                }else{
                    // 总页数大于分页长度（此时分为三种情况：1.左边没有...2.右边没有...3.左右都有...）
                    var offset = (conf.pagesLength - 1) / 2;  // 计算中心偏移量
                    if(conf.currentPage <= offset){
                        // 左边没有...
                        for(i = 1; i <= offset + 1; i++){
                            pageList.push(i);
                        }
                        pageList.push('...');
                        pageList.push(conf.numberOfPages);
                    }else if(conf.currentPage > conf.numberOfPages - offset){
                        pageList.push(1);
                        pageList.push('...');
                        for(i = offset + 1; i >= 1; i--){
                            pageList.push(conf.numberOfPages - i);
                        }
                        pageList.push(conf.numberOfPages);
                    }else{
                        // 最后一种情况，两边都有...
                        pageList.push(1);
                        pageList.push('...');

                        for(i = Math.ceil(offset / 2) ; i >= 1; i--){
                            pageList.push(conf.currentPage - i);
                        }
                        pageList.push(conf.currentPage);
                        for(i = 1; i <= offset / 2; i++){
                            pageList.push(conf.currentPage + i);
                        }

                        pageList.push('...');
                        pageList.push(conf.numberOfPages);
                    }
                }
                scope.pageList = pageList;
                //scope.$parent.conf = conf;
            }

            // 上一页
            scope.prevPage = function() {
                if(conf.currentPage > 1){
                    conf.currentPage -= 1;

                    getPagination();
                    if (conf.onChange) {
                        conf.onChange();
                    }
                }
            };

            // 下一页
            scope.nextPage = function() {
                if(conf.currentPage < conf.numberOfPages){
                    conf.currentPage += 1;

                    getPagination();
                    if (conf.onChange) {
                        conf.onChange();
                    }
                }
            };

            // 页码按钮点击事件
            scope.changeCurrentPage = function(item) {
                if(item == '...'){
                    return;
                } else if (item != conf.currentPage) {  //点击当前页不刷新
                    conf.currentPage = item;
                    getPagination();
                    if (conf.onChange) {
                        conf.onChange();
                    }
                }
            };

            //刷新当前页
            scope.refresh = function () {
                if (conf.onChange) {
                    conf.onChange();
                }
            };

            // 修改每页展示的条数
            scope.changeItemsPerPage = function() {
                // 一发展示条数变更，当前页将重置为1
                conf.currentPage = 1;
                getPagination();
                if(conf.onChange) {    
                    conf.onChange();
                }
            };

            // 跳转页
            scope.jumpToPage = function() {
                num = scope.jumpPageNum;
                if(num.match(/\d+/)) {
                    num = parseInt(num, 10);
                    if(num && num != conf.currentPage) {
                        if(num > conf.numberOfPages) {
                            num = conf.numberOfPages;
                        }
                        conf.currentPage = num;  // 跳转
                        getPagination();
                        if(conf.onChange) {    
                            conf.onChange();
                        }
                        scope.jumpPageNum = '';
                    }
                }
            };

            scope.jumpPageKeyUp = function(e) {
                var keycode = window.event ? e.keyCode :e.which;
                if(keycode == 13) {
                    scope.jumpToPage();
                }
            }

            scope.$watch('conf.totalItems', function (value, oldValue) {
                //if (!value || value == oldValue) {   // 在无值或值相等的时候，去执行onChange事件
                //    if(conf.onChange) {    
                //        conf.onChange();
                //    }
                //}

                if (value == oldValue) {   //第一次执行时
                    if (conf.onChange) {
                        conf.onChange();
                    }
                }
                getPagination();
            })
            
        }
    };
}]);
