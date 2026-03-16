var myapp = angular.module('ngAppDataRecycle', ['ui.grid','ui.grid.selection','ui.grid.resizeColumns','mdsdFilter','mdsdDirective','tm.pagination']);
myapp.controller('DataRecycleController', ['$scope', 'uiGridConstants',function ($scope, uiGridConstants) {
    $scope.hospitalList = hospitalList;
    $scope.comingTypeList = comingTypeList;
    $scope.diagnosis = diagnosisTypeArray;
    //分页配置
    $scope.paginationConf = {
        currentPage: 1,
        totalItems: 0,
        itemsPerPage: 20,
        pagesLength: 9,
        showJump:false,
        perPageOptions: [20, 50, 100],
        onChange: function(){
            $scope.searchParams.page = this.currentPage;
            $scope.searchParams.rows = this.itemsPerPage;
            $scope.search();
        }
    };
    //查询参数
    $scope.searchParams = {
        page:$scope.paginationConf.currentPage,
        rows:$scope.paginationConf.itemsPerPage,
        sort: "RegistDate",
        order: "desc",
        Name: "",
        OrgCode: "",
        DiagnosisType:"",
    };
    $scope.searchParams.OrgCode = ($scope.hospitalList != null && $scope.hospitalList.length == 1) ? $scope.hospitalList[0].Key : "";

    //点击查询
    $scope.searchClick = function(){
        $scope.paginationConf.currentPage = 1;
        $scope.searchParams.page = 1;
        $scope.search();
    }

    //搜索
    $scope.search = function () {
        $.ajax({
            type: 'POST',
            data: $scope.searchParams,
            datatype: "json",
            url: AddUrlPrefix("/DataCheck/QueryPatients/"),
            success: function (data) {
                if (data.IsSuccess) {
                    $scope.gridOptions.data = data.Data.rows;
                    $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);

                    $scope.paginationConf.totalItems =  data.Data.total;
                } else {
                    console.log("请求列表数据失败:" + data.Message);
                }
            }
        });
    }

    //批量删除
    $scope.operation = function (type) {
        var selectRows = $scope.gridApi.selection.getSelectedRows();
        var ids = selectRows.map(function(entity){
            return entity.Id;
        })

        if (ids.length == 0) {
            layer.alert("请选择要操作的记录", { title: '友情提示', icon: 0, closeBtn: 0, move: false });
            return;
        }
        if(type==1)
        {
            $scope.batchDelete(ids);
        }
        else if(type==2)
        {
            $scope.batchRestore(ids);
        }
    }

    //删除操作
    $scope.batchDelete = function (ids) {
        layer.confirm("确认删除吗，删除后数据将不可恢复？", { icon: 3, title: '提示' }, function (index) {
            $.ajax({
                type: 'POST',
                data: { ids: ids },
                datatype: "json",
                url: AddUrlPrefix("/DataCheck/DeletePatients/"),
                success: function (data) {
                    if (data.IsSuccess) {
                        layer.msg("删除成功！", {time : 1000});
                        $scope.search(); //刷新列表
                    } else {
                        layer.alert(data.Message, { title: '删除失败', icon: 2, closeBtn: 0, move: false });
                    }
                }
            });
            layer.close(index);
        });
    }

    //批量恢复
    $scope.batchRestore = function (ids) {
        layer.confirm("确认恢复吗？", { icon: 3, title: '提示' }, function (index) {
            $.ajax({
                type: 'POST',
                data: { ids: ids },
                datatype: "json",
                url: AddUrlPrefix("/DataCheck/RestorePatients/"),
                success: function (data) {
                    if (data.IsSuccess) {
                        layer.msg("恢复成功！", { time: 1000 });
                        $scope.search(); //刷新列表
                    } else {
                        layer.alert(data.Message, { title: '恢复失败', icon: 2, closeBtn: 0, move: false });
                    }
                }
            });
            layer.close(index);
        });
    }

    //列表选项
    $scope.gridOptions = {
        useExternalSorting: true,
        enableRowSelection: true,
        enableColumnResizing:true,
        enableColumnMenus:false,  //菜单栏不现实下拉菜单
        columnDefs: [
          { field: 'ClinicNo',displayName:'病例号',width:'15%', enableSorting: false},
          { field: 'Name',displayName:'姓名',width:'10%', suppressRemoveSort:true,sortDirectionCycle:[uiGridConstants.ASC, uiGridConstants.DESC] },
          { field: 'Age',displayName:'年龄',width:'8%', enableSorting: false  },
          { field: 'Gender',displayName:'性别',width:'8%', enableSorting: false ,cellFilter: 'getValue:' + genderArrayJsonStr },
          //{ field: 'CreateDate', displayName: '创建时间', width: '10%', sort: { direction: uiGridConstants.DESC, priority: 1 }, suppressRemoveSort: true, sortDirectionCycle: [uiGridConstants.ASC, uiGridConstants.DESC], cellFilter: 'dateFmt' },
          { field: 'PhoneNo1', displayName: '联系方式', width: 120, align: 'center' },
          {
              field: 'RegistDate', displayName: '创建时间', width: 150, align: 'center', sortable: true, formatter: function (value, row) {
                  if (value == null || typeof value == "undefined") {
                      return "";
                  }
                  return getTime(value);
              }
          },
          {
              field: 'DiseaseTime', displayName: '发病时间', width: 150, align: 'center', sortable: true, formatter: function (value, row) {
                  if (value == null || typeof value == "undefined") {
                      return "";
                  }
                  return getTime(value);
              }
          },
          { field: 'DiagnosisType', displayName: '诊断类型', width: '10%', enableSorting: false, cellFilter: 'getValue:' + diagnosisTypeArrayJsonStr },
          { field: 'OrgCode', displayName: '医院', width: '15%', enableSorting: false, cellFilter: 'getValue:' + hospitalListJsonStr },

        ],
        onRegisterApi: function(gridApi) {
            $scope.gridApi = gridApi;

            $scope.gridApi.core.on.sortChanged( $scope, function ( grid, sortColumns ){//监听排序条件的改变
                if (sortColumns.length > 1) {
                    // We have more than one sort. Kill the first one.
                    // If this works we'll only ever have 0, 1 or 2 sortColumns,
                    // and only ever 2 for the lifetime of this method.
                    var column = null;
                    for (var j = 0; j < grid.columns.length; j++) {
                        if (grid.columns[j].field === sortColumns[0].field) {
                            column = grid.columns[j];
                            break;
                        }
                    }
                    if (column) {
                        sortColumns[1].sort.priority = 1; // have to do this otherwise the priority keeps going up.
                        column.unsort();
                    }
                }
                lastSort = sortColumns[sortColumns.length - 1];
                $scope.searchParams.sort = lastSort.field;
                $scope.searchParams.order = lastSort.sort.direction;
                $scope.search();  //调用搜索
            });
        }
    };
}]);
angular.bootstrap($(".content")[0], ['ngAppDataRecycle']);
var tempData = { isEdit: false };