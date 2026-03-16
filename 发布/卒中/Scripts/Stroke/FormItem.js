
    function FormItem(item) {
        this.item = item;
    }

    // 取得选项内容html,需要子类实现
    FormItem.prototype.getPropertyHtml = function () {
        return CreateOptionHtml(this.item);
    }

    // 主方法
    FormItem.prototype.toHtml = function () {
        var _html = "<div class='topic_block'>";
        _html += "<input type='hidden' name='Item_ID' value='" + this.item.ID + "'>";
        _html += "<input type='hidden' name='Item_KeyCode' value='" + this.item.KeyCode + "'>";
        _html += "<input type='hidden' name='Item_Name' value='" + this.item.Name + "'>";
        _html += "<input type='hidden' name='Item_Type' value='" + this.item.Type + "'>";
        _html += "<input type='hidden' name='Item_Title' value='" + this.item.Title + "'>";
        _html += "  <div class='tit_bar line_b'>" + this.item.Title + "</div>";
        // 按钮栏
        _html += this.getBtnPanelHtml();
        // 选项内容
        _html += this.getPropertyHtml();
        // 校验内容
        _html += this.getValidateHtml();

        _html += "</div>";
        return _html;
    }

    // 获取校验信息
    FormItem.prototype.getValidateHtml = function () {
        var _html = "";
        var item = this.item;
        if(item.Property != undefined && 
            item.Property.ValidateRules != undefined &&
            item.Property.ValidateRules.length > 0)
        {
            $.each(this.item.Property.ValidateRules, function (index, rule) {
                _html = "<p class='line_t p_t5 rule'>";
                _html += "<input type='hidden' name='Rule_Name' value='" + rule.Name + "'>";
                _html += "<input type='hidden' name='Rule_Type' value='" + rule.Type + "'>";
                _html += "<input type='hidden' name='Rule_Message' value='" + rule.Message + "'>";
                _html += CreateRuleHtml(rule.Type,item,rule);
                _html += "</p>";
            });
        }
        return _html;
    }

    // 取得按钮信息
    FormItem.prototype.getBtnPanelHtml = function () {
        var _html = "";
        _html += "    <div class='topic_btn_grp'>";
        _html += "      <a href='avascript:;' class='itemUp' onClick='ItemUp(this)' title='上移'>上移</a>";
        _html += "      <a href='avascript:;' class='itemDown' onClick='ItemDown(this)' title='下移'>下移</a>";
        _html += "      <a href='avascript:;' onClick='ItemRemove(this)' title='删除'>删除</a>";
        _html += "      <a href='javascript:;' onClick='createItem(this)' title='新建'>新建</a>";
        _html += "      <a href='javascript:;' onClick='editItem(this)' title='编辑'>编辑</a>";
        _html += "      <a href='javascript:;' onClick='selectFromLib(this)' title='从题库添加'>从题库添加</a>";
        _html += "    </div>";
        return _html;
    }

