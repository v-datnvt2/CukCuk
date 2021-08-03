<template>
  <div>
    <FieldInputLabel
      inputType="text"
      id="Email"
      labelText="Ngày gia nhập công ty"
      fieldName="JoinDate"
      dataType="Date"
      tabIndex="14"
      required="false"
      :mydata="employee.JoinDate"
      :employee="employee"
      placeHolder=""
      @inputting="userInput"
    />
    <ButtonIcon
      subclass="btn-save"
      iconName="icon-save"
      buttonText="Lưu"
      @btn-click="btnSaveOnClick"
    />
    <Table />
  </div>
</template>
<script>
import FieldInputLabel from "./child.vue";
import ButtonIcon from "../../components/base/BaseButtonIcon.vue";
import Table from "../../components/base/BaseTable.vue";
export default {
  name: "parent",
  components: {
    FieldInputLabel,
    ButtonIcon,
    Table,
  },
  data() {
    return {
      parentInput: "",
      employee: {},
    };
  },
  methods: {
    /**
     * Vue chỉ dùng để thử, hoạt động,
     */
    /**
     * Vue chỉ dùng để thử, hoạt động,
     */
    btnSaveOnClick() {
      let abc = {};
      abc = { FullName: "hùng", JoinDate: Math.random() * 1000 + "" };
      this.employee = abc;

      // this.$set(this.employee, 'JoinDate', Math.random()*1000+"")
      console.log(this.employee.JoinDate);
    },
    userInput(text, fieldName, res) {
      this.employee[fieldName] = text;
      console.log("res:,",text, fieldName, res);
    },
  },
};
</script>