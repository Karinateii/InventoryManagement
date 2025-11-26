# Partial Fulfillment Tracking

## What it does

Tracks partial shipments of purchase orders. If you order 100 units but receive them in two shipments (50 now, 50 later), the system now tracks:
- How much has been received
- How much is still outstanding  
- Fulfillment percentage

## Implementation

### 1. Database Changes
**Migration:** `20251126220745_AddQuantityReceivedToPurchaseOrder`

Added new column to `PurchaseOrders` table:
- `QuantityReceived` (int) - Tracks cumulative quantity received, defaults to 0

### 2. Model Updates
**File:** `Inventory.Models/Models/PurchaseOrder.cs`

Added properties:
- `QuantityReceived` (int) - Stored in database
- `QuantityRemaining` (calculated) - QuantityOrdered - QuantityReceived
- `IsFullyReceived` (bool, calculated) - True when all items received
- `FulfillmentPercentage` (decimal, calculated) - Completion percentage (0-100%)

### 3. View Updates
**File:** `Areas/User/Views/PurchaseOrders/Index.cshtml`

Added columns to show:
- Quantity Received
- Quantity Remaining
- Fulfillment % (with progress bar)
  - Green progress bar when fully received (100%)
  - Blue progress bar when partially received (<100%)

### 4. Stock Adjustment Updates

#### ViewModel Update
**File:** `Inventory.Models/ViewModels/StockAdjustmentVM.cs`

Added:
- `PurchaseOrderID` (int?) - Optional link to purchase order

#### Controller Update
**File:** `Areas/Admin/Controllers/StockAdjustmentController.cs`

**Index Method:**
- Now loads pending/partially received purchase orders for the supply
- Passes them to the view via ViewBag

**Adjust Method:**
- When adding stock with a purchase order linked:
  - Validates quantity doesn't exceed what's remaining
  - Increments `QuantityReceived` on the purchase order
  - Auto-updates order status:
    - "Partially Received" - when some but not all items received
    - "Received" - when fully received (IsFullyReceived = true)
  - Logs the fulfillment details

#### View Update
**File:** `Areas/Admin/Views/StockAdjustment/Adjust.cshtml`

Added:
- Purchase Order dropdown (shown when reason is "Purchase Order Received")
- Shows PO details: Ordered, Received, Remaining quantities
- Auto-suggests remaining quantity when PO selected
- Forces "Add" adjustment type when receiving PO items

## Usage

### Receiving Stock

1. Go to Admin → Lab Supplies
2. Click on the supply you're receiving
3. Click "Adjust Stock"
4. Select "Purchase Order Received"
5. Choose the PO from the dropdown
6. Enter quantity (defaults to remaining amount)
7. Submit

The system validates you don't receive more than ordered and auto-updates the PO status.

## Database Changes

```sql
ALTER TABLE PurchaseOrders 
ADD QuantityReceived int NOT NULL DEFAULT 0;
```

New calculated properties:
- `QuantityRemaining` = QuantityOrdered - QuantityReceived
- `IsFullyReceived` = QuantityReceived >= QuantityOrdered
- `FulfillmentPercentage` = (QuantityReceived / QuantityOrdered) × 100
