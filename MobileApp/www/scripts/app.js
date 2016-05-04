var app = { // ana sınıf oluşturuldu
    token: "C3N8zj73cgoGN0Q_19hvp5OT05QDWKM2-00hejsl9VCfo_7SpEDGMd1NbQj_oJvOHrPSJRAnEo7ZBP75wvOd82boU6qcv0w19CE1YG_SLyfO_VA29bEcbEbGs-2QNIfehcdzn2gUDyzehsgFojtH2BG7NRqzqzfBmH0u93HSh-3LRnIombFMa1onplURW9vWJjGLpQrAvuL0bdJUquZnXLVz5-kbStr8-2SNHDNDu4Km-xIgkRJ9HsKhgGN0XzqxLMVSPkvduX4IO6fCMT2j3yW0NvtH_t38DTPRBnqlUTgSGSSf52IK747VRHiWT4ZW1qkFfG0OAgvglUnk4jPpLpsel60znO-yehseCjibIF21EkM4bvx0ujzk-NhqAUFg516QpMemVzuwmzubymtzo6co46yEvWL1AWP96fuA-zAeKfbHDqHER8MusTC2JerZPVu0fP8E1VKijXO2PIBE7FKpNF9gQsShl_n2PaGzeJHn5nB4I4-VkZhnlHqinhGa",
    // token hazırlandı
    apiHostAddress: "http://valuetics-apps.cloudapp.net:8088",

    query: function (method, url, data, successFunc, errorFuncOpt) {
        var _data;
        if (data) _data = data;
        $.ajax({
            method: method,
            url: app.apiHostAddress + url,
            data: _data,
            contentType: "application/json; charset=utf-8",
            success: successFunc,
            error: function (err) {
                if (!errorFuncOpt) console.log(err);
                else errorFuncOpt();
            }
        });
    },

    queryWithToken: function (method, url, data, successFunc, errorFuncOpt) {
        var _data;
        if (data) _data = data;
        $.ajax({
            method: method,
            url: app.apiHostAddress + url,
            data: _data,
            contentType: "application/json; charset=utf-8",
            headers: { "Authorization": 'Bearer ' + token },
            success: successFunc,
            error: function (err) {
                if (!errorFuncOpt) console.log(err);
                else errorFuncOpt();
            }
        });
    },

    createDropdown: function (className) {
        $(className).dropdown({
            inDuration: 300,
            outDuration: 225,
            constrain_width: false, // Does not change width of dropdown to that of the activator
            hover: false, // Activate on hover
            gutter: 0, // Spacing from edge
            belowOrigin: false, // Displays dropdown below the button
            alignment: 'left' // Displays dropdown with edge aligned to the left of button
        });
    },

    createSideNavigation: function (className) {
        $(className).sideNav();
    },

    createLeanModal: function (className) {
        $(className).leanModal();
    }

}



//PRODUCTS
products = {
    //DATABASE OPERATIONS
    query: {

        getAll: function (successFunc) {
            app.query("get", "/api/products/getAll", null, successFunc);
        },

        getByCategory: function (categoryId, successFunc) {
            app.query("get", "/api/products/getByCategory/" + categoryId, null, successFunc);
        },

        getById: function (productId, successFunc) {
            app.query("get", "/api/products/getById/" + productId, null, successFunc);
        },

        getByCategories: function (successFunc) {
            app.query("get", "/api/productCategories/getProductCategories", null, successFunc);
        }
    },

    //TEMPLATING OPERATIONS
    view: {

        //ADMIN TEMPLATES
        createAdminListView: function (elementId) {
            return function (data) {
                var html = "";

                var createRow = function (product) {

                    html += "<tr class='collection-item' style='font-size:20px!important'><td><a href='productDetails.html'><img class='circle' src='content/products/images/yuna.jpg'></a></td><td class='center-align'><a href='productDetails.html'>" + product.name + "</a></td> <td class='right-align'>" +
                        '<a class="modal-trigger btn-floating btn-large brown lighten-2 " onclick="products.screen.selectedProductId=' + product.id + '" href="#menu"><i style="color:#fff!important" class="material-icons">mode_edit</i></a>'
                        + "</td></tr>";
                }

                if (data.constructor === Array) {
                    for (var i = 0; i < data.length; i++) {
                        createRow(data[i]);
                    }
                }
                else {
                    createRow(data);
                }
                document.getElementById(elementId).innerHTML = html;
                //app.createLeanModal('.modal-trigger');
                $('.modal-trigger').leanModal();
            }
        },

        createAdminCategoryList: function (elementId) {
            return function (data) {
                var categoryList = "";
                
                var creaetList = function (category) {
                    categoryList += "<option value='" + category.id + "'>" + category.name + "</option>";
                }
                if (data.constructor === Array) {
                    for (i = 0; i < data.length; i++) {
                        creaetList(data[i]);
                    }
                } else {
                    creaetList(data);
                }
                document.getElementById(elementId).innerHTML = categoryList;
            }
        }


        //ANONYMOUS TEMPLATES

    },

    screen: {
        selectedProductId: -1
    }


}

users = {
    query: {
        login: function (username, password) {
            var loginSuccess = function () {
                window.location = "/products.html"
            }
            var loginFailed = function () {
                document.getElementById("error-box").innerHTML = "<div class='card-panel red lighten-1 blue-text text-darken-4'>Kullanıcı adı veya şifre yanlış!</div>";
            }

            //Pseudo Code
            var returnFunc = loginFailed;
            if (username = 1) returnFunc = loginSuccess;

            app.query("post", "/users/login", { "username": username, "password": password }, returnFunc, errorFunc)
        },

        logout: function () {
            var successFunc = function () {
                window.location = "/??"
            }
            var errorFunc = function () {
                //???
            }
            app.queryWithToken("post", "/users/logout", null, successFunc, errorFunc)
        }
    }
}

//PRODUCT CATEGORIES
//////categories = {
//////    //db operations
//////    query: {
//////        getAll: function (successFunc) {
//////            app.query("get", "/api/products/getAll", null, successFunc);
//////        }
//////    }
//////}