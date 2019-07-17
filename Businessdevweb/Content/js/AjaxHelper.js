"use-strict";

var elementActions = document.querySelectorAll(".ajaxAction");

for (let i = 0; i < elementActions.length; i++) {
    elementActions[i].classList.remove("ajaxAction");
    elementActions[i].addEventListener("click",
        function (e) {
      
            e.preventDefault();
            let el = e.currentTarget;
            let ajaxAction = el.getAttribute("action");
            let ajaxMethod = el.getAttribute("data-method");
            let ajaxFailureFunction = el.getAttribute("failure-func");
            let ajaxSuccessFunction = el.getAttribute("success-func");
            let ajaxContentType = el.getAttribute("data-content-type");
            let ajaxDataId = el.getAttribute("data-id");
            let ajaxDataArea = el.getAttribute("data-area");
            let ajaxAlert = el.getAttribute("ajax-alert");
            if (ajaxAlert === "true") {


                swal({
                   
                    title: el.getAttribute("alert-title"),
                    text: el.getAttribute("alert-text"),  //"در صورت حذف رکورد، دیگر قابل بازیابی نمی باشد!",   
                    type: el.getAttribute("alert-type"),
                    showCancelButton: true,
                    confirmButtonColor: "#e6b034",
                    confirmButtonText: el.getAttribute("alert-confirm"),  //"بله، حذف بشه!",   
                    cancelButtonText: el.getAttribute("alert-cancel"), // "نه، لغو بشه!",   
                    closeOnConfirm: false,
                    closeOnCancel: false
                }, function (isConfirm) {
                    if (isConfirm) {
                        $.ajax({
                            url: "/" + ajaxDataArea + ajaxAction + "/" + ajaxDataId ,

                            type: ajaxMethod,
                            processData: false,  // Important!
                            contentType: false,
                            cache: false,
                            //contentType:ajaxContentType,
                           
                            success: function (data) {
                      
                                if (ajaxSuccessFunction !== "") {
                                    swal({
                                        title: data.title,
                                        type: data.type,
                                        text: data.message,
                                        timer: 3000,
                                        showConfirmButton: false
                                    });
                                    
                                    window[ajaxSuccessFunction](data);
                                } else {
                                 
                                        swal({
                                            title: data.title,
                                            type: data.type,
                                            text: data.message,
                                            timer: 3000,
                                            showConfirmButton: false
                                        });
                                    
                                }
                            },
                            error: function (data) {
                                if (ajaxFailureFunction !== "") {
     
                                    window[ajaxFailureFunction](data);
                                } else {
                                    swal({
                                        title: "خطا!",
                                        type: "error",
                                        text: "خطای غیر قابل پیش بینی، با پشتیبانی تماس بگیرید! ",
                                        confirmButtonText: "بله، حتما"
                                    });
                            
                                }

                            }
                        });

                    }
                    else {
                        swal({
                            title: "لغو!",
                            type: "error",
                            text: "عملیات توسط شما لغو شد؟",
                            confirmButtonText: "بله"
                        });
                    }
                });

            } else {
                $.ajax({
                    url: "/" + ajaxDataArea + ajaxAction + "/" + ajaxDataId,
                    type: ajaxMethod,
                    processData: false,  // Important!
                    contentType: false,
                    cache: false,

                    success: function (data) {
                        if (ajaxSuccessFunction !== "") {

                            window[ajaxSuccessFunction](data);
                        } else {
                            alert("success");
                        }
                    },
                    error: function (data) {
                        if (ajaxFailureFunction !== "") {
          
                            window[ajaxFailureFunction](data);
                        } else {
                            alert("Error");
                        }

                    }
                });

            }

        });
}
var elements = document.querySelectorAll(".ajaxForm");
for (let i = 0; i < elements.length; i++) {
    elements[i].classList.remove("ajaxForm");
    elements[i].addEventListener("submit",
        function (e) {

            debugger;
            e.preventDefault();
            var isAllow = true;
            let el = e.currentTarget;

            let formData = new FormData(this);
            $(el).find("input[name]").each(function (index, node) {

                formData.append(node.name, node.value);
                if (node.value === "" || node.value === null) {
                    node.style.borderColor = "red";
                    node.addEventListener('change', function (e) {
                        if (e.srcElement.value.trim() === "" || e.srcElement.value === null) {
                            e.srcElement.setAttribute("style", "border-color:red");
                        }
                        else { e.srcElement.removeAttribute("style"); }

                    });
                    isAllow = false;
                }
            });


            let ajaxAction = el.getAttribute("action");
            let ajaxMethod = el.getAttribute("method");
            let ajaxFailureFunction = el.getAttribute("failure-func");
            let ajaxSuccessFunction = el.getAttribute("success-func");
  let enctype = el.getAttribute("enctype");

            let autoReset = el.getAttribute("data-autoReset");
            if (isAllow === true) {
                $.ajax({

                    url: ajaxAction,
                    type: ajaxMethod,
                    processData: false,  // Important!
                    contentType: false,
                    cache: false,
                    enctype: enctype,
                    data: formData,
                    success: function (data) {

                        if (ajaxSuccessFunction !== "") {
                            window[ajaxSuccessFunction](data);
                            if (autoReset === "True") {
                                $(el).find("input[name]").each(function (index, node) {

                                    node.value = null;
                                });
                            }
                        } else {
                            if (autoReset === "True") {
                                $(el).find("input[name]").each(function (index, node) {

                                    node.value = null;
                                });
                            }
                            alert("success");
                        }
                    },
                    error: function (data) {
                        if (ajaxFailureFunction !== "") {
                            failureFunc(data);
                        } else {

                            alert("Error");
                        }

                    }
                });

            }

        });

}