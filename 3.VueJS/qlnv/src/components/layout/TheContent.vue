<template>
  <div>
    <!-- <EmployeePage @showAddForm="showAddForm" />
    <AddEmployeeForm
      @hideAddForm="hideAddForm"
      v-if="showForm"
      ref="AddEmployeeForm"
    /> -->
  </div>
</template>
<script>
import EmployeePage from "../../view/employee/EmployeePage.vue";
import AddEmployeeForm from "../../view/employee/AddEmployeeForm.vue";
export default {
  name: "Content",
  data() {
    return {
      showForm: false,
    };
  },
  components: {
    EmployeePage,
    AddEmployeeForm,
  },
  methods: {
    // showAddForm() {
    //   this.showForm = true;
    // },
    // hideAddForm() {
    //   this.showForm = false;
    // },
  },
  //$refs.AddEmployeeForm.showEm
};
</script>