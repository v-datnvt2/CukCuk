<template>
  <div>
    <div class="content" :class="{'expanded': contentExpanded}">
      <div class="content-header" :class="{'expanded': contentExpanded}">
        <div class="title">Danh sách nhân viên</div>

        <div class="head-button-zone">
          <ButtonIcon
            subclass="danger"
            iconName="icon-toast-danger"
            buttonText="Xóa dữ liệu"
            @btn-click="$refs.ctable.btnDeleteOnClick()"
            v-if="totalSelected > 0"
          />

          <ButtonIcon
            subclass="add-entity"
            iconName="icon-add"
            buttonText="Thêm nhân viên"
            @btn-click="showAddForm"
          />
        </div>
      </div>
      <div class="toolbar" :class="{'expanded': contentExpanded}">
        <div class="filter">
          <FieldInputIcon
            subclass="searchfield"
            iconName="icon-search"
            placeHolder="Tìm theo mã, Tên hoặc Số điện thoại"
          />
          <Dropdown
            id="main-division"
            subclass="division"
            myurl="api/Department"
            itemId="DepartmentId"
            itemName="DepartmentName"
            defaultName="Tất cả phòng ban"
          />
          <Dropdown
            id="main-position"
            subclass="position"
            myurl="v1/Positions"
            itemId="PositionId"
            itemName="PositionName"
            defaultName="Tất cả vị trí"
          />
        </div>
        <ButtonIcon
          @btn-click="$refs.ctable.loadTableData()"
          iconName="icon-refresh"
        />
      </div>
      <Table
        ref="ctable"
        :contentExpanded="contentExpanded"
        :warningResponse="warningResponse"
        @showWarningPopup="showWarningPopup"
        @dbClickOnTR="dbClickOnTR"
        @checkOnItem="checkOnItem"
        @showToastMessage="showToastMessage"
        @callReloadTable="$refs.ctable.loadTableData()"
      />
      <PageNavigation :contentExpanded= "contentExpanded"/>
    </div>
    <AddEmployeeForm
      :isHidden="isHidden"
      :employeeId="employeeId"
      :formMode="formMode"
      :toggleForm="toggleForm"
      :warningResponse="warningResponse"
      @callReloadTable="$refs.ctable.loadTableData()"
      @hideAddForm="hideAddForm"
      @showWarningPopup="showWarningPopup"
      @showToastMessage="showToastMessage"
    />
    <WarningPopup
      :popupMessage="popupMessage"
      :isHiddenWarning="isHiddenWarning"
      :action="action"
      @btn-cancel="popupCancel"
      @btn-confirm="popupConfirm"
    />
    <div class="toast-message-zone" ref="toastMessageZone">
      <ToastMessage
        :isHiddenToastMessage="isHiddenToastMessage"
        :toastMessage="toastMessage"
        @btn-close="closeToastMessage"
      />
    </div>
  </div>
</template>

<script>
// import Vue from "vue";
import Dropdown from "../../components/base/BaseDropdown.vue";
import ButtonIcon from "../../components/base/BaseButtonIcon.vue";
import FieldInputIcon from "../../components/base/BaseFieldInputIcon.vue";
import PageNavigation from "../../components/base/PageNavigation.vue";
import Table from "./EmployeeTable.vue";
import AddEmployeeForm from "./AddEmployeeForm.vue";
import WarningPopup from "../../components/base/BaseWarningPopup.vue";
import ToastMessage from "../../components/base/BaseToastMessage.vue";
export default {
  name: "EmployeePage",
  components: {
    Dropdown,
    ButtonIcon,
    FieldInputIcon,
    Table,
    AddEmployeeForm,
    PageNavigation,
    WarningPopup,
    ToastMessage,
  },
  props: {
    //content props
    contentExpanded: Boolean,
  },
  data() {
    return {
      //form props
      isHidden: true,
      toggleForm: true,
      totalSelected: 0,
      employeeId: "",
      formMode: -1,
      //popup props
      popupMessage: {},
      action: "",
      isHiddenWarning: true,
      warningResponse: "",

      //toastmessage props
      isHiddenToastMessage: true,
      toastMessage: {},
    };
  },
  methods: {
    /**
     * Đổi giá trị biến để hiển thị form thông tin, thêm mới
     * gán formmode= 0 ( 0: thêm mới ,  1: cập nhật)
     * Nguyễn Hùng 30/07
     */
    showAddForm() {
      this.isHidden = false;
      this.formMode = 0;
      this.toggleForm = !this.toggleForm;
    },

    /**
     * Đổi giá trị biến để ẩn form thông tin
     * Nguyễn Hùng 30/07
     */
    hideAddForm() {
      this.isHidden = true;
      this.formMode = -1;
    },

    /**
     * Đổi giá trị biến để hiển thị form thông tin, cập nhật
     * gán formmode= 1 ( 0: thêm mới ,  1: cập nhật)
     * Nguyễn Hùng 30/07
     */
    dbClickOnTR(employeeId) {
      this.employeeId = employeeId;
      this.formMode = 1;
      this.isHidden = false;
      this.toggleForm = !this.toggleForm;
      console.log(employeeId);
    },

    /**
     * Gán dữ liệu từ sự kiên emit vào biến, để truyền xuống component con
     * Hiển thị popup
     * Nguyễn Hùng 30/07
     */
    showWarningPopup(popupMessage, action) {
      this.isHiddenWarning = false;
      this.popupMessage = popupMessage;
      this.action = action;
    },

    showToastMessage(toastMessage) {
      this.isHiddenToastMessage = false;
      this.toastMessage = toastMessage;
    },

    // doubleToast() {
    //   this.isHiddenToastMessage = false;

    //   let mytoast = {};
    //   mytoast.toastText = "Hellloooo";
    //   mytoast.toastType = "Warning";
    //   var ButtonComponent = Vue.extend(ToastMessage);
    //   let newinstance = new ButtonComponent({
    //     toastMessage: mytoast,
    //   });
    //   newinstance.$mount();
    //   this.$refs.toastMessageZone.appendChild(newinstance.$el);
    // },

    closeToastMessage() {
      this.isHiddenToastMessage = true;
    },
    /**
     * Sự kiện emit khi bấm nút cancel, close thì ẩn popup
     * Ẩn popup
     * Nguyễn Hùng 30/07
     */
    popupCancel() {
      this.isHiddenWarning = true;
    },

    /**
     * Sự kiện emit khi bấm nút confirm, gán thông điệp vào props dể gửi về component con.
     * Ẩn popup
     * Nguyễn Hùng 30/07
     */
    popupConfirm(action) {
      let timestamp = new Date();
      this.warningResponse = `Confirm${action}_${timestamp}`;
      this.isHiddenWarning = true;
    },

    /**
     * Thực hiện cập nhật số lượng item được check khi check vào checkbox, table Emit.
     */
    checkOnItem(selectedCount) {
      this.totalSelected = selectedCount;
    },
  },
};
</script>

<style scoped>
@import "../../css/layout/content.css";
.footer .navigation-button .button {
  width: 32px !important;
  min-width: 32px !important;
}
</style>