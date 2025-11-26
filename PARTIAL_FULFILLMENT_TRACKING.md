# Purchase Order Partial Fulfillment Tracking

## Overview
This feature allows you to track partial fulfillment of purchase orders. When you receive shipments in multiple batches (e.g., ordering 100 units but receiving 50 now and 50 later), the system now tracks:
- How much has been received
- How much is still outstanding
- The fulfillment percentage

## Changes Made

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

## How It Works

### Scenario: Your Purchase Order
You mentioned: "i made a purchase order of 100 when i had just 50 then i made the stock adjustment of 50"

**Before (Old System):**
- Purchase Order: 100 units ordered
- Stock Adjustment: Added 50 units
- ❌ No tracking of how much of the PO was received

**After (New System):**
1. **Create Purchase Order:** 100 units
   - Status: Pending
   - Quantity Ordered: 100
   - Quantity Received: 0
   - Quantity Remaining: 100
   - Fulfillment: 0%

2. **First Shipment Arrives (50 units):**
   - Go to Stock Adjustment
   - Select "Purchase Order Received" as reason
   - Select the purchase order from dropdown
   - Enter quantity: 50
   - Submit
   
   **Result:**
   - Stock increased by 50
   - PO updated:
     - Status: Partially Received
     - Quantity Received: 50
     - Quantity Remaining: 50
     - Fulfillment: 50%

3. **Second Shipment Arrives (50 units):**
   - Repeat stock adjustment for remaining 50 units
   
   **Result:**
   - Stock increased by 50 more
   - PO updated:
     - Status: Received (auto-changed)
     - Quantity Received: 100
     - Quantity Remaining: 0
     - Fulfillment: 100%

## Usage Instructions

### Viewing Purchase Order Status
1. Navigate to **Purchase Orders** (User menu)
2. The index page shows all purchase orders with:
   - Qty Ordered
   - Qty Received
   - Qty Remaining
   - Visual progress bar showing fulfillment percentage

### Receiving Purchase Order Items
1. Navigate to **Admin → Lab Supplies**
2. Click on the supply you're receiving
3. Click "Adjust Stock"
4. Select **"Purchase Order Received"** as the reason
5. **Purchase Order dropdown appears:**
   - Shows all pending/partially received POs for this supply
   - Displays ordered, received, and remaining quantities
6. Select the purchase order
7. System auto-fills the remaining quantity (you can change it)
8. Click "Confirm Adjustment"

### Validation
The system prevents:
- Receiving more than ordered
- Shows error: "Cannot receive more than ordered. Ordered: X, Already Received: Y"

## Benefits

1. **Full Traceability:** Know exactly how much of each PO has been received
2. **Accurate Inventory:** Stock adjustments are linked to purchase orders
3. **Status Automation:** Order status updates automatically based on fulfillment
4. **Visual Progress:** Progress bars show fulfillment at a glance
5. **Prevents Over-Receiving:** Validates you don't receive more than ordered

## Testing Your Scenario

To test with your existing data:
1. Run the application
2. View your purchase order in the Purchase Orders list
3. You should now see:
   - Quantity Received: 0 (since it was created before this feature)
   - Quantity Remaining: 100
   - Fulfillment: 0%
4. To fix historical data, do a stock adjustment:
   - Go to the supply
   - Adjust stock → "Purchase Order Received"
   - Select your PO
   - Enter 50 (the amount you already received)
   - This will update the PO to show 50 received, 50 remaining

## Database Schema

```sql
-- New column added to PurchaseOrders table
ALTER TABLE PurchaseOrders 
ADD QuantityReceived int NOT NULL DEFAULT 0;
```

## Notes

- The feature is backward compatible - existing purchase orders default to 0 received
- You can optionally link stock adjustments to POs (not required)
- Manual stock adjustments (not from POs) still work the same way
- Status changes are automatic based on fulfillment percentage
