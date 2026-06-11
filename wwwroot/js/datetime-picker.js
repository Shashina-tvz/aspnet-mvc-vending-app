function initDateTimePickers() {
  document.querySelectorAll(".datetime-wrapper").forEach((wrapper) => {
    const hidden = wrapper.querySelector(".datetime-value");
    const visible = wrapper.querySelector(".datetime-picker");

    if (!hidden || !visible) return;

    if (!hidden.value) {
      visible.classList.add("invalid-field");
    } else {
      visible.classList.remove("invalid-field");
    }

    visible.addEventListener("change", function () {
      if (visible.value) {
        visible.classList.remove("invalid-field");
      }
    });
  });
}

document.addEventListener("DOMContentLoaded", initDateTimePickers);
