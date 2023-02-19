# __Microsoft.Devices.HardwareDevCenterManager__

![Nuget](https://img.shields.io/nuget/v/Microsoft.Devices.HardwareDevCenterManager)
![Nuget](https://img.shields.io/nuget/dt/Microsoft.Devices.HardwareDevCenterManager)
[![Build Validation](https://github.com/microsoft/devices-hardware-dev-center-manager/actions/workflows/dotnet-pr-build-validation.yml/badge.svg)](https://github.com/microsoft/devices-hardware-dev-center-manager/actions/workflows/dotnet-pr-build-validation.yml)

Class library used in invoking HTTP requests to the [Hardware Dev Center dashboard API](https://learn.microsoft.com/en-us/windows-hardware/drivers/dashboard/dashboard-api)

---

## __Features__
### Manage Products
```csharp
DevCenterResponse<Product> response = await api.NewProduct(json);
DevCenterResponse<Product> response = await api.GetProducts(ProductId);
```
---

### Manage Submissions
```csharp
DevCenterResponse<Submission> response = await api.NewSubmission(ProductId, json);
DevCenterResponse<bool> response = await api.CommitSubmission(ProductId, SubmissionId);
DevCenterResponse<Submission> response = await api.GetSubmission(ProductId, SubmissionId);
```

---

### Manage Shipping Labels
```csharp
DevCenterResponse<ShippingLabel> response = await api.NewShippingLabel(ProductId, SubmissionId, json);
DevCenterResponse<ShippingLabel> response = await api.GetShippingLabels(ProductId, SubmissionId, ShippingLabelId);
```

---

### Download Packages
```csharp
DevCenterResponse<Submission> response = await api.GetSubmission(ProductId, SubmissionId);
// use download links from submission resource
```

---

### Create Metadata
```csharp
DevCenterResponse<bool> response = await api.CreateMetaData(ProductId, SubmissionId);
```

---

## __Contributing__

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
