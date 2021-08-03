<template>
  <div>
    <div :class="{ pagecover: !isHidden }"></div>
    <div
      class="popup add-entity"
      entityId="manv"
      :class="{ isnone: isHidden }"
      :employeeId="employeeId"
      :formMode="formMode"
    >
      <div class="header">
        <div class="title">THÔNG TIN NHÂN VIÊN</div>
        <Button subclass="btn-close" @btn-click="callCloseForm" />
      </div>
      <div class="employee-info">
        <div class="avatar">
          <div class="avatar-image"></div>
          <p>(Vui lòng chọn ảnh có định</p>
          <p>dạng</p>
          <p>.jpg, .jpeg, .png, .gif.)</p>
          <input type="file" name="avatar-file" id="avatar-file" />
        </div>
        <div class="detail">
          <div class="general-detail">
            <div class="title">A. THÔNG TIN CHUNG</div>
            <div class="hs"></div>
            <div class="grid">
              <div class="grid-row">
                <div class="grid-col">
                  <FieldInputLabel
                    labelText="Mã nhân viên"
                    fieldName="EmployeeCode"
                    tabIndex="1"
                    placeHolder="NV-0000"
                    required="true"
                    :mydata="employee.EmployeeCode"
                    v-model="employee.EmployeeCode"
                  />
                </div>
                <div class="grid-col">
                  <FieldInputLabel
                    labelText="Họ và tên"
                    fieldName="FullName"
                    tabIndex="2"
                    dataType="HumanName"
                    required="true"
                    :mydata="employee.FullName"
                    v-model="employee.FullName"
                  />
                </div>
              </div>
              <div class="grid-row">
                <div class="grid-col">
                  <FieldInputLabel
                    inputType="date"
                    labelText="Ngày sinh"
                    fieldName="DateOfBirth"
                    tabIndex="3"
                    dataType="Date"
                    required="false"
                    :mydata="formatDateYMD(employee.DateOfBirth)"
                    v-model="employee.DateOfBirth"
                  />
                </div>
                <div class="grid-col">
                  <Dropdown
                    labelText="Giới tính"
                    id="Gender"
                    subclass="Gender"
                    itemId="Gender"
                    itemName="GenderName"
                    :selectedId="employee.Gender + ''"
                    v-model="employee.Gender"
                  />
                </div>
              </div>
              <div class="grid-row">
                <div class="grid-col">
                  <FieldInputLabel
                    id="IdentityNumber"
                    labelText="Số CMTND/ Căn cước"
                    fieldName="IdentityNumber"
                    dataType="IdentityNumber"
                    tabIndex="5"
                    required="true"
                    placeHolder="9/12 chữ số"
                    :mydata="employee.IdentityNumber"
                    v-model="employee.IdentityNumber"
                  />
                </div>
                <div class="grid-col">
                  <FieldInputLabel
                    inputType="date"
                    id="IdentityDate"
                    labelText="Ngày cấp"
                    fieldName="IdentityDate"
                    dataType="IdentityDate"
                    tabIndex="6"
                    :mydata="formatDateYMD(employee.IdentityDate)"
                    v-model="employee.IdentityDate"
                  />
                </div>
              </div>
              <div class="grid-row">
                <div class="grid-col">
                  <FieldInputLabel
                    id="IdentityPlace"
                    labelText="Nơi cấp"
                    fieldName="IdentityPlace"
                    tabIndex="7"
                    required="false"
                    :mydata="employee.IdentityPlace"
                    v-model="employee.IdentityPlace"
                  />
                </div>
              </div>
              <div class="grid-row">
                <div class="grid-col">
                  <FieldInputLabel
                    id="Email"
                    labelText="Email"
                    fieldName="Email"
                    dataType="Email"
                    tabIndex="8"
                    required="true"
                    placeHolder="youremail@example.com"
                    :mydata="employee.Email"
                    v-model="employee.Email"
                  />
                </div>
                <div class="grid-col">
                  <FieldInputLabel
                    id="Email"
                    labelText="Số điện thoại"
                    fieldName="PhoneNumber"
                    dataType="PhoneNumber"
                    tabIndex="8"
                    required="true"
                    placeHolder=""
                    :mydata="employee.PhoneNumber"
                    v-model="employee.PhoneNumber"
                  />
                </div>
              </div>
            </div>
          </div>
          <div class="job-detail">
            <div class="title">B. THÔNG TIN CÔNG VIỆC</div>
            <div class="hs"></div>
            <div class="grid">
              <div class="grid-row">
                <div class="grid-col">
                  <Dropdown
                    labelText="Vị trí"
                    id="main-position"
                    subclass="position"
                    myurl="v1/Positions"
                    itemId="PositionId"
                    itemName="PositionName"
                    :selectedId="employee.PositionId"
                    v-model="employee.PositionId"
                  />
                </div>
                <div class="grid-col">
                  <Dropdown
                    labelText="Phòng ban"
                    id="main-division"
                    subclass="division"
                    myurl="api/Department"
                    itemId="DepartmentId"
                    itemName="DepartmentName"
                    :selectedId="employee.DepartmentId + ''"
                    v-model="employee.DepartmentId"
                  />
                </div>
              </div>
              <div class="grid-row">
                <div class="grid-col">
                  <FieldInputLabel
                    id="PersonalTaxCode"
                    labelText="Mã số thuế cá nhân"
                    fieldName="PersonalTaxCode"
                    dataType="PersonalTaxCode"
                    tabIndex="12"
                    required="true"
                    placeHolder=""
                    :mydata="employee.PersonalTaxCode"
                    v-model="employee.PersonalTaxCode"
                  />
                </div>
                <div class="grid-col">
                  <FieldInputCurrency
                    id="Salary"
                    labelText="Mức lương cơ bản"
                    fieldName="Salary"
                    tabIndex="13"
                    currency="VND"
                    placeHolder=""
                    :mydata="employee.Salary + ''"
                    v-model="employee.Salary"
                  />
                </div>
              </div>
              <div class="grid-row">
                <div class="grid-col">
                  <FieldInputLabel
                    inputType="date"
                    id="Email"
                    labelText="Ngày gia nhập công ty"
                    fieldName="JoinDate"
                    dataType="Date"
                    tabIndex="14"
                    required="false"
                    placeHolder=""
                    :mydata="formatDateYMD(employee.JoinDate)"
                    v-model="employee.JoinDate"
                  />
                </div>
                <div class="grid-col">
                  <label>Tình trạng công việc</label>
                  <Dropdown
                    id="WorkStatus"
                    subclass="WorkStatus"
                    myurl=""
                    itemId="WorkStatus"
                    itemName="WorkStatusName"
                    :selectedId="employee.WorkStatus + ''"
                    v-model="employee.WorkStatus"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="footer">
        <ButtonIcon
          subclass="btn-save"
          iconName="icon-save"
          buttonText="Lưu"
          @btn-click="btnSaveOnClick"
        />
        <Button subclass="cancel" buttonText="Hủy" @btn-click="callCloseForm" />
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import { CommonFn } from "../../js/mixins.js";
import FieldInputLabel from "../../components/base/BaseFieldInputLabel.vue";
import FieldInputCurrency from "../../components/base/BaseFieldInputCurrency.vue";
import Dropdown from "../../components/base/BaseDropdown.vue";
import ButtonIcon from "../../components/base/BaseButtonIcon.vue";
import Button from "../../components/base/BaseButton.vue";
export default {
  mixins: [CommonFn],
  name: "EmployeeForm",
  components: {
    Dropdown,
    FieldInputLabel,
    ButtonIcon,
    Button,
    FieldInputCurrency,
  },
  data() {
    return {
      employee: {},
      // EmployeeCode: "",
    };
  },
  props: {
    isHidden: Boolean,
    employeeId: String,
    formMode: Number,
    toggleForm: Boolean,
    warningResponse: String,
  },
  methods: {
    /**
     * Hàm thực thi cho sự kiện input emit bởi ô input
     * lấy nội dung + tên thuộc tính của ô input đó, gán vào thuộc tính tương ứng của entity
     * Nguyễn Hùng 30/07
     */
    userInput(text, fieldName) {
      this.employee[fieldName] = text;
      console.log(text, fieldName);
    },

    /**
     * Hàm thực thi cho sự kiện emit bởi dropdown khi chọn item
     * lấy nội dung + id của item đó, gán vào thuộc tính tương ứng của entity
     * Nguyễn Hùng 30/07
     */
    userSelect(itemIdValue, itemId) {
      this.employee[itemId] = itemIdValue;
      console.log(itemIdValue, itemId);
    },

    /**
     * Thực thi cho sự kiện do nút save emit .
     * Kiểm tra trạng thái form thêm mới (0) hay cập nhật (1)
     * Gọi API tương ứng để lưu dữ liệu
     * todo validate
     *  Nguyễn Hùng 30/07
     */
    btnSaveOnClick() {
      let message = {},
        action = "SaveEntity";
      message["warningType"] = "Notify";
      message["text-before"] = "Bạn có muốn lưu dữ liệu nhân viên ";
      message["text-target"] = this.employee.FullName;
      message["text-after"] = "không ?";
      message["confirm-button-text"] = "Xác nhận";

      // console.log("callsave", message);
      this.$emit("showWarningPopup", message, action);
    },

    confirmSaveOnClick() {
      let vm = this;

      console.log("Saved employee", vm.employee);
      if (vm.formMode == 0) {
        axios
          .post("http://cukcuk.manhnv.net/v1/Employees/", vm.employee)
          .then(() => {
            vm.generateToastMessage("Thêm mới dữ liệu thành công", "Notify");
            vm.$emit("hideAddForm");
            vm.$emit("callReloadTable");
          })
          .catch((error) => {
            console.log(error);
            vm.generateToastMessage("Lỗi khi thêm mới", "Danger");
          });
      } else {
        axios
          .put(
            `http://cukcuk.manhnv.net/v1/Employees/${vm.employeeId}`,
            vm.employee
          )
          .then(() => {
            vm.generateToastMessage("Cập nhật thông tin thành công", "Notify");
            vm.$emit("hideAddForm");
            vm.$emit("callReloadTable");
          })
          .catch((error) => {
            console.log(error);
            vm.generateToastMessage("Lỗi khi cập nhật", "Danger");
          });
      }
    },

    generateToastMessage(toastText, toastType) {
      let toastMessage = {};

      toastMessage.toastText = toastText;
      toastMessage.toastType = toastType;
      this.$emit("showToastMessage", toastMessage);
    },
    getNewEmployeeCode() {
      let vm = this;
      axios
        .get("http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode")
        .then((response) => {
          let newEntity = {
            EmployeeCode: "",
            PositionId: "",
            DepartmentId: "",
            Gender: "",
            WorkStatus: "",
          };
          newEntity.EmployeeCode = response.data;
          vm.employee = newEntity;
          console.log(vm.employee.EmployeeCode);
        })
        .catch((error) => {
          console.log(error);
          vm.generateToastMessage(
            "Không thể lấy được mã nhân viên mới, lỗi server",
            "Danger"
          );
        });
    },
    /**
     * Khởi tạo nội dung message
     * Emit sự kiện để gọi hàm hiển thị popup khi bấm nút thoát
     */
    callCloseForm() {
      let message = {},
        action = "CloseForm";
      message["warningType"] = "Warning";
      message["text-before"] = "Dữ liệu chưa được lưu";
      message["text-target"] = " sẽ bị mất. ";
      message["text-after"] = "Xác nhận đóng form thông tin nhân viên ?";
      message["confirm-button-text"] = "Xác nhận";

      // console.log("callcloseform", message);
      this.$emit("showWarningPopup", message, action);
    },

    resetEntityData() {
      let vm = this,
        newEntity = {
          EmployeeCode: "",
          PositionId: "",
          DepartmentId: "",
          Gender: "",
          WorkStatus: "",
        };
      vm.employee = newEntity;
    },
  },
  watch: {
    /**
     * Theo dõi khi id của đối tượng truyền vào thay đổi
     * gọi API lấy dữ liệu của đối tượng tương ứng với ID mới
     * Nguyễn Hùng 30/07
     */
    toggleForm: function () {
      let vm = this;
      vm.resetEntityData();
      if (vm.formMode == 1) {
        axios
          .get(`http://cukcuk.manhnv.net/v1/Employees/${vm.employeeId}`)
          .then((response) => {
            let foundEmployee = response.data;
            // vm.resetEntityData();

            vm.employee = foundEmployee;
          })
          .catch((error) => {
            console.log(error);
          });
      } else {
        // let newEntity = {};
        // vm.employee = newEntity;
        this.getNewEmployeeCode();
      }
    },
    warningResponse: function () {
      let response = this.warningResponse.split("_");
      // console.log("listen Form", response);
      switch (response[0]) {
        case "ConfirmCloseForm":
          this.$emit("hideAddForm");
          console.log("hideAddForm");
          break;
        case "ConfirmSaveEntity":
          this.confirmSaveOnClick();
      }
    },
  },
};
</script>